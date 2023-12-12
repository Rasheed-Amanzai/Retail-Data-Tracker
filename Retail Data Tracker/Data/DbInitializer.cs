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

            // Order Entities
            var orders = new Order[] {
                new Order() { Id = 1, Items = new List<Item>(items), Quantity = new List<Quantity>(),
                    TrackingNumber = "ABC123456789", OrderDate = new DateTime(2023, 5, 20, 0, 0, 0, DateTimeKind.Utc), ShippingDate = new DateTime(2023, 5, 23, 0, 0, 0, DateTimeKind.Utc),
                    ArrivalDate = new DateTime(2023, 5, 25, 0, 0, 0, DateTimeKind.Utc), OrderClient = new Client()},
                new Order() { Id = 2, Items = new List<Item>(items), Quantity = new List<Quantity>(),
                    TrackingNumber = "XYZ987654321", OrderDate = new DateTime(2023, 10, 12, 0, 0, 0, DateTimeKind.Utc), ShippingDate = new DateTime(2023, 10, 13, 0, 0, 0, DateTimeKind.Utc),
                    ArrivalDate = new DateTime(2023, 10, 13, 0, 0, 0, DateTimeKind.Utc), OrderClient = new Client() }, 
                new Order() { Id = 3, Items = new List<Item>(items), Quantity = new List<Quantity>(),
                    TrackingNumber = "QWE789456123", OrderDate = new DateTime(2023, 4, 3, 0, 0, 0, DateTimeKind.Utc), ShippingDate = new DateTime(2023, 4, 5, 0, 0, 0, DateTimeKind.Utc),
                    ArrivalDate = new DateTime(2023, 4, 7, 0, 0, 0, DateTimeKind.Utc), OrderClient = new Client() }
            };

            foreach (var o in orders) 
            {  
                context.Orders.Add(o); 
            }
            context.SaveChanges();

            // Client Entities
            var clients = new Client[]{ 
                new Client() { Id = 1, Name = "Food Basics", Address = "4652 Grocery Street",
                    Description = "Food Basics is a grocery store chain that provides a wide selection of essential food items and household products at affordable prices.",
                    ClientOrder = new List<Order>(orders)},
                new Client() { Id = 2, Name = "Loblaws", Address = "372 Competing Drive",
                    Description = "Loblaws is a Canadian supermarket chain offering a diverse range of groceries, fresh produce, household goods, and services in a modern retail environment.",
                    ClientOrder = new List<Order>(orders)},
                new Client() { Id = 3, Name = "Sobeys", Address = "3842 Alsogrocery Avenue",
                    Description = "Sobeys is a Canadian grocery chain known for its quality fresh produce, groceries, and household items.",
                    ClientOrder = new List<Order>(orders)}
            };

            foreach (var c in clients) 
            {
                context.Client.Add(c);
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
