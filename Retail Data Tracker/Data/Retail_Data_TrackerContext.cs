/// Written by Matthew-Nocera-Iozzo (GitHub: @afk-m) ///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Retail_Data_Tracker.Models;

namespace Retail_Data_Tracker.Data
{
    public class Retail_Data_TrackerContext : DbContext
    {
        public Retail_Data_TrackerContext (DbContextOptions<Retail_Data_TrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Retail_Data_Tracker.Models.Item> Items { get; set; } = default!;
        public DbSet<Retail_Data_Tracker.Models.Supplier> Suppliers { get; set; } = default!;
        public DbSet<Retail_Data_Tracker.Models.Order> Orders { get; set; } = default!;
        public DbSet<Retail_Data_Tracker.Models.Client> Client { get; set; } = default!;
        public object OrderItems { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ItemId });

            // Other entity configurations...

            base.OnModelCreating(modelBuilder);
        }

    }
}
