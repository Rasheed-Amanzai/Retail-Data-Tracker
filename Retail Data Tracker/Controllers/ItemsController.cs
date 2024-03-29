﻿/// Written by Matthew-Nocera-Iozzo (GitHub: @afk-m) ///

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Retail_Data_Tracker.Data;
using Retail_Data_Tracker.Models;

namespace Retail_Data_Tracker.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Retail_Data_TrackerContext _context;

        public ItemsController(Retail_Data_TrackerContext context)
        {
            _context = context;
        }

        public ItemClientViewModel generateModel()
        {
            var items = _context.Items.ToList();
            var itemSuppliers = new List<Supplier>();

            foreach (var item in items)
            {
                var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == item.SupplierId);
                itemSuppliers.Add(supplier);
                Console.WriteLine(supplier.Name);
            }

            var viewModel = new ItemClientViewModel
            {

                Items = _context.Items.ToList(),
                Clients = _context.Client.ToList(),
                Suppliers = itemSuppliers
            };

            return viewModel;
        }

        // GET: Items
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = generateModel();
            //var items = _context.Items.Include(i => i.ItemSupplier);
            return View(viewModel);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ItemClientViewModel model, string submitButton)
        {
            var items = model.Items;
            var client = _context.Client.Find(model.SelectedClientId);
            Console.WriteLine($"{client.Name}");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name} - IsChecked: {item.IsChecked}");
            }
            if (submitButton == "invoiceSubmit") {
                //Console.WriteLine(submitButton);
                //ViewBag.Message = "Invoice Created";
                var checkedItems = items.Where(x => x.IsChecked).ToList();

                if (checkedItems.Count == 0)
                {
                    ViewBag.Message = "Please select an item to order";
                    var lModel = generateModel();
                    return View(lModel);
                }

                foreach (var item in checkedItems)
                {
                    Console.WriteLine(item.Id);
                    var itemx = await _context.Items.FindAsync(item.Id);
                    if (itemx.Quantity == 0)
                    {
                        ViewBag.Message = "Not enough items in stock";
                        var lModel = generateModel();
                        return View(lModel);
                    }
                }

                foreach (var item in checkedItems)
                {
                    var itemx = await _context.Items.FindAsync(item.Id);
                    Console.WriteLine(item.Name);
                    ViewBag.Message += string.Format(" {0}", item.Name);
                    //item.Quantity -= 1;
                    //itemx.Quantity -= 1;
                    //_context.Update(itemx);
                    //await _context.SaveChangesAsync();
                }
                Order order = new Order();
                _context.Client.Attach(client);

                foreach (var item in checkedItems)
                {
                    var itemToUpdate = _context.Items.Find(item.Id);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Quantity -= itemToUpdate.QuantityWanted;
                        _context.Update(itemToUpdate);
                        OrderItem orderItem = new OrderItem
                        {
                            Item = itemToUpdate,
                            Order = order,
                            QuantityNumber = item.QuantityWanted
                        };
                        order.OrderItems.Add(orderItem);
                        Console.WriteLine("Yes!:" + order.OrderItems.Count);
                        await _context.SaveChangesAsync();
                    }
                }

                order.OrderClient = client;
                order.TrackingNumber = RandomString(8);
                order.OrderDate = DateTime.Now;
                order.ShippingDate = DateTime.Now.AddDays(1);
                order.ArrivalDate = DateTime.Now.AddDays(2);
                order.OrderType = OrderType.Retailer;

                Console.WriteLine(order.OrderItems.Count);
                Console.WriteLine(order.OrderTotal);


                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            } else if (submitButton == "orderFromSupplier"){
                //Console.WriteLine(submitButton);
                //ViewBag.Message = "Invoice Created";

                var checkedItems = items.Where(x => x.IsChecked).ToList();

                if (checkedItems.Count > 1)
                {
                    for (var i = 1; i < checkedItems.Count; i++)
                    {
                        Console.WriteLine(checkedItems[i].SupplierId);
                        Console.WriteLine(checkedItems[i-1].SupplierId);
                        if (checkedItems[i].SupplierId != checkedItems[i - 1].SupplierId)
                        {
                            ViewBag.Message = "Supplier order can only be from one supplier.";
                            var lModel = generateModel();
                            return View(lModel);
                        }
                    }
                }

                if (checkedItems.Count == 0)
                {
                    ViewBag.Message = "Please select an item.";
                    var lModel = generateModel();
                    return View(lModel);
                }

                foreach (var item in checkedItems)
                {
                    Console.WriteLine(item.Id);
                    var itemx = await _context.Items.FindAsync(item.Id);
                    if (itemx.Quantity == 0)
                    {
                    }
                }

                foreach (var item in checkedItems)
                {
                    var itemx = await _context.Items.FindAsync(item.Id);
                    Console.WriteLine(item.Name);
                    ViewBag.Message += string.Format(" {0}", item.Name);
                    //item.Quantity -= 1;
                    //itemx.Quantity -= 1;
                    //_context.Update(itemx);
                    //await _context.SaveChangesAsync();
                }
                Order order = new Order();
                _context.Client.Attach(client);

                foreach (var item in checkedItems)
                {
                    var itemToUpdate = _context.Items.Find(item.Id);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Quantity -= itemToUpdate.QuantityWanted;
                        _context.Update(itemToUpdate);
                        OrderItem orderItem = new OrderItem
                        {
                            Item = itemToUpdate,
                            Order = order,
                            QuantityNumber = item.QuantityWanted
                        };
                        order.OrderItems.Add(orderItem);
                        Console.WriteLine("Yes!:" + order.OrderItems.Count);
                        await _context.SaveChangesAsync();
                    }
                }

                order.OrderClient = _context.Client.FirstOrDefault(x => x.Name == "You");
                order.TrackingNumber = RandomString(8);
                order.OrderDate = DateTime.Now;
                order.ShippingDate = DateTime.Now.AddDays(1);
                order.ArrivalDate = DateTime.Now.AddDays(2);
                order.OrderType = OrderType.Supplier;

                Console.WriteLine(order.OrderItems.Count);
                Console.WriteLine(order.OrderTotal);


                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            }

            var lmodel = generateModel();
            return View(lmodel);
        }
        
        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new ItemCreateViewModel
            {
                Suppliers = _context.Suppliers.ToList()
            };
            return View(viewModel);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.SupplierId);
            
                Console.WriteLine(item.BuyCost);
                item.QuantityWanted = 0;
                item.IsChecked = false;
                var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == item.SupplierId);
                item.ItemSupplier = supplier;

                _context.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "Id", "Id", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ItemDesc,Quantity,SupplierId,BuyCost,SellCost")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            var viewModel = new ItemCreateViewModel
            {
                Suppliers = _context.Suppliers.ToList()
            };
            return View(viewModel);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'Retail_Data_TrackerContext.Item'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
