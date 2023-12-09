using Retail_Data_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Retail_Data_Tracker.Data
{
    public class RetailContext : DbContext
    { 
        public RetailContext(DbContextOptions<RetailContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        // Add DbSets for other entity/model classes (Item, Order, etc.)

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
