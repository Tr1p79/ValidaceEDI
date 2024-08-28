using System;
using System.Xml;
using System.Xml.Schema;
using ValidaceEDI.Data;
using ValidaceEDI.Models;

public class EDIProccesor
{
    static void Main(string[] args)
    {

        string xmlPath = "path\\to\\ValidaceEDI\\test.xml";
        string xsdPath = "path\\to\\ValidaceEDI\\Schema\\Objednavka.xsd"; 

        string xmlContent = File.ReadAllText(xmlPath);

        try
        {
            ProcessXml(xmlContent, xsdPath);
            Console.WriteLine("XML úspěšně zpracováno a ověřeno.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void ProcessXml(string xmlContent, string xsdPath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        // Načtení a ověření podle schématu XSD
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add("test", xsdPath); 

        xmlDoc.Schemas = schemaSet;
        xmlDoc.Validate((Sender, arg) =>
        {
            Console.WriteLine(arg.Message);
        });

        // Extrahuje a ukládá data do databáze
        using (var context = new EDIContext())
        {
            var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("ns", "test");

            var orderNode = xmlDoc.SelectSingleNode("/ns:Order", namespaceManager);

            if (orderNode == null)
            {
                throw new InvalidOperationException("Order element not found in the XML.");
            }

            var order = new Order
            {
                OrderDate = DateTime.Parse(orderNode.SelectSingleNode("ns:OrderDate", namespaceManager)?.InnerText ?? throw new InvalidOperationException("OrderDate is missing or invalid"))
            };

            var itemNodes = orderNode.SelectNodes("ns:Item", namespaceManager);
            foreach (XmlNode itemNode in itemNodes)
            {
                var item = new Item
                {
                    ProductName = itemNode.SelectSingleNode("ns:ProductName", namespaceManager)?.InnerText,
                    Quantity = int.Parse(itemNode.SelectSingleNode("ns:Quantity", namespaceManager)?.InnerText)
                };
                order.Items.Add(item);
            }

            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}