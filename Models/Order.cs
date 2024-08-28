using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidaceEDI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

    }

    public class Item
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
