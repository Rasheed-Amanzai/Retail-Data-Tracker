using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            // Item entities
            context.Items.AddRange(
                new Item() { Id = 1, Name = "Wheat Grain", ItemDesc = "Raw wheat grain", BuyCost = 0.10, SellCost = 1.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 2, Name = "1kg All-Purpose Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 3, Name = "Whole Wheat Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 4, Name = "White Bread Loaf", ItemDesc = "Unsliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 5, Name = "Sliced Whole Wheat Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 6, Name = "Sliced White Bread Loaf", ItemDesc = "Sliced", BuyCost = 0.75, SellCost = 3.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 7, Name = "1kg Whole Wheat Flour", ItemDesc = "Flour good for any of your baking needs", BuyCost = 0.25, SellCost = 2.00, Quantity = 100, SupplierId = 1 },
                new Item() { Id = 8, Name = "500g Ranch Croutons", ItemDesc = "Croutons with a ranch flavor", BuyCost = 1.00, SellCost = 5.00, Quantity = 100, SupplierId = 1 }
            );

            // Order Entities
            context.Orders.AddRange(
                new Order()
                {
                    Id = 1,
                    Items = new List<Item>(),
                    Quantity = new List<int>(),
                    TrackingNumber = "ABC123456789",
                    OrderDate = "May 20th, 2023",
                    ShippingDate = "May 22nd, 2023",
                    ArrivalDate = "May 25th, 2023",
                    OrderClient = new Client()
                },
                new Order()
                {
                    Id = 2,
                    Items = new List<Item>(),
                    Quantity = new List<int>(),
                    TrackingNumber = "XYZ987654321",
                    OrderDate = "October 12th, 2023",
                    ShippingDate = "October 13th, 2023",
                    ArrivalDate = "October 15th, 2023",
                    OrderClient = new Client()
                },
                new Order()
                {
                    Id = 3,
                    Items = new List<Item>(),
                    Quantity = new List<int>(),
                    TrackingNumber = "QWE789456123",
                    OrderDate = "April 3rd, 2023",
                    ShippingDate = "April 5th, 2023",
                    ArrivalDate = "April 7th, 2023",
                    OrderClient = new Client()
                }
            );

            context.Client.AddRange(
                new Client()
                {
                    Id = 1,
                    Name = "Food Basics",
                    Address = "4652 Grocery Street",
                    Description = "Food Basics is a grocery store chain that provides a wide selection of essential food items and household products at affordable prices.",
                    ClientOrder = new List<Order>()
                },
                new Client()
                {
                    Id = 2,
                    Name = "Loblaws",
                    Address = "372 Competing Drive",
                    Description = "Loblaws is a Canadian supermarket chain offering a diverse range of groceries, fresh produce, household goods, and services in a modern retail environment.",
                    ClientOrder = new List<Order>()
                },
                new Client()
                {
                    Id = 3,
                    Name = "Sobeys",
                    Address = "3842 Alsogrocery Avenue",
                    Description = "Sobeys is a Canadian grocery chain known for its quality fresh produce, groceries, and household items.",
                    ClientOrder = new List<Order>()
                }
                );

            context.Suppliers.AddRange(
                new Supplier()
                {
                    Id = 1,
                    Name = "Winston's Wild Wheats",
                    Address = "123 Farmer Road",
                    Description = "A small farm that grows wheat and other grain products.",
                    SupplierInventory = new List<Item>()
                },
                new Supplier()
                {
                    Id = 2,
                    Name = "Frank's Fruits",
                    Address = "456 Plantation Drive",
                    Description = "A large farm that produces a wide variety of fruit products.",
                    SupplierInventory = new List<Item>()
                }, // TODO: Add fruit items to inventory
                new Supplier()
                {
                    Id = 3,
                    Name = "Vennessa's Veggies",
                    Address = "789 Vine Avenue",
                    Description = "A small farm that grows fresh vegetables.",
                    SupplierInventory = new List<Item>()
                } // TODO: Add vegetable items to inventory
            );
        }
    }
}