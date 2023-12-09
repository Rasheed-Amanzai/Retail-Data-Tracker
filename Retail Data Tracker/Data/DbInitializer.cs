using Retail_Data_Tracker.Models;
using System;
using System.Linq;

namespace Retail_Data_Tracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Retail_Data_TrackerContext context)
        {
            context.Database.EnsureCreated();

            // Check if there are any suppliers
            if (context.Suppliers.Any())
            {
                return; // Database has already been seeded
            }

            // Item entities
            var items = new Item[]
            {
                new Item() {Id = 1, Name = "Wheat Grain", ItemDesc = "Raw wheat grain", BuyCost = 0.10, SellCost = 1.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 2, Name = "1kg All-Purpose Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 3, Name = "Whole Wheat Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 4, Name = "White Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 5, Name = "Sliced Whole Wheat Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 6, Name = "Sliced White Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 7, Name = "1kg Whole Wheat Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1},
                new Item() {Id = 8, Name = "500g Ranch Croutons", ItemDesc = "Croutons with a ranch flavor", BuyCost = 1.00, SellCost = 5.00, Quantity = 100, SupplierId = 1},
            };

            foreach (var i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

            // Supplier entities
            var suppliers = new Supplier[]
            {
                new Supplier() {Id = 1, Name = "Winston's Wild Wheats", Address = "123 Farmer Road", 
                    Description = "A small farm that grows wheat and other grain products.", SupplierInventory = new List<Item>(items) },
                new Supplier() {Id = 2, Name = "Frank's Fruits", Address = "456 Plantation Drive",
                    Description = "A large farm that produces a wide variety of fruit products.", SupplierInventory = new List<Item>(items) }, // TODO: Add fruit items to inventory
                new Supplier() {Id = 3, Name = "Vennessa's Veggies", Address = "789 Vine Avenue",
                    Description = "A small farm that grows fresh vegetables.", SupplierInventory = new List<Item>(items) } // TODO: Add vegetable items to inventory
            };

            foreach (var s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            // Continue with other entities (Order, Client, etc.)
        }
    }
}
