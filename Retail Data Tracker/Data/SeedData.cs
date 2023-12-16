using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Retail_Data_Tracker.Controllers;
using Retail_Data_Tracker.Models;
using System;
using System.Linq;

namespace Retail_Data_Tracker.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Retail_Data_TrackerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<Retail_Data_TrackerContext>>()))
        {
            context.Database.EnsureCreated();

            // Check if there are any suppliers
            if (context.Suppliers.Any())
            {
                return; // Database has already been seeded
            }

            context.Suppliers.AddRange(
                new Supplier()
                {
                    Name = "Winston's Wild Wheats",
                    Address = "123 Farmer Road",
                    Description = "A small farm that grows wheat and other grain products.",
                    SupplierInventory = new List<Item>()
                },
                new Supplier()
                {
                    Name = "Frank's Fruits",
                    Address = "456 Plantation Drive",
                    Description = "A large farm that produces a wide variety of fruit products.",
                    SupplierInventory = new List<Item>(),
                }, // TODO: Add fruit items to inventory
                new Supplier()
                {
                    Name = "Vennessa's Veggies",
                    Address = "789 Vine Avenue",
                    Description = "A small farm that grows fresh vegetables.",
                    SupplierInventory = new List<Item>(),
                } // TODO: Add vegetable items to inventory
            );
            context.SaveChanges();

            // Item entities
            context.Items.AddRange(
                new Item() { Name = "Wheat Grain", ItemDesc = "Raw wheat grain", BuyCost = 0.10, SellCost = 1.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "1kg All-Purpose Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "Whole Wheat Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "White Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "Sliced Whole Wheat Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "Sliced White Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "1kg Whole Wheat Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "500g Ranch Croutons", ItemDesc = "Croutons with a ranch flavor", BuyCost = 1.00, SellCost = 5.00, Quantity = 100, SupplierId = 1 },
                new Item() { Name = "Papaya", ItemDesc = "A tropical fruit", BuyCost = 0.50, SellCost = 2.00, Quantity = 100, SupplierId = 2 }
            );
            context.SaveChanges();

            context.Client.AddRange(
                new Client()
                {
                    Name = "Food Basics",
                    Address = "4652 Grocery Street",
                    Description = "Food Basics is a grocery store chain that provides a wide selection of essential food items and household products at affordable prices.",
                    ClientOrder = new List<Order>()
                },
                new Client()
                {
                    Name = "Loblaws",
                    Address = "372 Competing Drive",
                    Description = "Loblaws is a Canadian supermarket chain offering a diverse range of groceries, fresh produce, household goods, and services in a modern retail environment.",
                    ClientOrder = new List<Order>()
                },
                new Client()
                {
                    Name = "Sobeys",
                    Address = "3842 Alsogrocery Avenue",
                    Description = "Sobeys is a Canadian grocery chain known for its quality fresh produce, groceries, and household items.",
                    ClientOrder = new List<Order>()
                },
                new Client()
                {
                    Name = "You",
                    Address = "1234 Your Street",
                    Description = "Our Service!",
                    ClientOrder = new List<Order>()
                }
                );
            context.SaveChanges();

            //// Order Entities
            //context.Orders.AddRange(
            //    new Order()
            //    {
            //        OrderItems = new List<OrderItem> { context.Items.Where(i => i.Id == 1).FirstOrDefault() },
            //        Quantity = new List<Quantity>(),
            //        TrackingNumber = "ABC123456789",
            //        OrderDate = new DateTime(2023, 5, 20, 0, 0, 0, DateTimeKind.Utc),
            //        ShippingDate = new DateTime(2023, 5, 23, 0, 0, 0, DateTimeKind.Utc),
            //        ArrivalDate = new DateTime(2023, 5, 25, 0, 0, 0, DateTimeKind.Utc),
            //        OrderClient = context.Client.Where(i => i.Id == 1).FirstOrDefault()
            //    },
            //    new Order()
            //    {
            //        Items = new List<Item> { context.Items.Find(1), context.Items.Find(3) },
            //        Quantity = new List<Quantity>(),
            //        TrackingNumber = "XYZ987654321",
            //        OrderDate = new DateTime(2023, 10, 12, 0, 0, 0, DateTimeKind.Utc),
            //        ShippingDate = new DateTime(2023, 10, 13, 0, 0, 0, DateTimeKind.Utc),
            //        ArrivalDate = new DateTime(2023, 10, 15, 0, 0, 0, DateTimeKind.Utc),
            //        OrderClient = context.Client.Where(i => i.Id == 2).FirstOrDefault()

            //    },
            //    new Order()
            //    {
            //        Items = new List<Item> { context.Items.Find(4), context.Items.Find(2) },
            //        Quantity = new List<Quantity>(),
            //        TrackingNumber = "QWE789456123",
            //        OrderDate = new DateTime(2023, 4, 4, 0, 0, 0, DateTimeKind.Utc),
            //        ShippingDate = new DateTime(2023, 4, 5, 0, 0, 0, DateTimeKind.Utc),
            //        ArrivalDate = new DateTime(2023, 4, 7, 0, 0, 0, DateTimeKind.Utc),
            //        OrderClient = context.Client.Where(i => i.Id == 3).FirstOrDefault()
            //    }
            //);
            //context.SaveChanges();


        }
    }
}