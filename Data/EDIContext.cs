using Microsoft.EntityFrameworkCore;
using ValidaceEDI.Models;

namespace ValidaceEDI.Data
{
    public class EDIContext : DbContext
    {
        // Definice DbSetů pro tabulky orders a OrderItems
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }


        //Konfigurace připojení k databázi
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            optionsBuilder.UseSqlServer("Server=YOUR_SERVER_NAME;Database=EDIValidaceDB;Trusted_Connection=True;TrustServerCertificate=True;"); 
        }

        // Definice modelů a vztahů mezi nimi
        /// <summary>
        /// Každá objednávka 'Order' může obsahovat více položek 'items'. relace (1:N)
        /// Vztah je reprezentován cizím klíčem "OrderId" v tabulce 'items', který odkazuje na primární klíč "OrderId" v tabulce 'Orders'.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
        }
    }
}
