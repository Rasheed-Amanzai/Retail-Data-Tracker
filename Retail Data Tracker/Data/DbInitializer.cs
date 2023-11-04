using Retail_Data_Tracker.Models;
using System;
using System.Linq;

namespace Retail_Data_Tracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RetailContext context)
        {
            context.Database.EnsureCreated();

            // Check if there are any suppliers
            if (context.Suppliers.Any())
            {
                return; // Database has already been seeded
            }

            // TODO: Populate the database
            var suppliers = new Supplier[]
            {
                new Supplier() {Id = 1, Name = "Bob's Wheat Farm", Address = "123 Farmer Rd", Description = "A small farm that grows wheat and other grain products."} // Missing Inventory
            };

            foreach (var s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            // Continue with other entity/models (Item, Order, etc.)
        }
    }
}
