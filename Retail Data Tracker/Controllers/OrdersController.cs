using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Retail_Data_Tracker.Data;
using Retail_Data_Tracker.Models;

namespace Retail_Data_Tracker.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Retail_Data_TrackerContext _context;

        public OrdersController(Retail_Data_TrackerContext context)
        {
            _context = context;
        }




        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return _context.Orders.Include(i => i.OrderClient).ToList() != null ?
                        View(await _context.Orders.ToListAsync()) :
                        Problem("Entity set 'Retail_Data_TrackerContext.Orders'  is null.");
        }
        
        [HttpGet, ActionName("ItemDetails")]
        public async Task<IActionResult> ItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                                      .Include(o => o.Items)
                                      .Include(o => o.Quantity) // Include quantities
                                      .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailsViewModel
            {
                ItemQuantities = new List<ItemQuantityViewModel>(),
                OrderTotal = order.OrderTotal
            };

            for (int i = 0; i < order.Items.Count; i++)
            {
                viewModel.ItemQuantities.Add(new ItemQuantityViewModel
                {
                    Item = order.Items[i],
                    Quantity = order.Quantity[i].QuantityNumber
                });
            }

            return View(viewModel);
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }
            
            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            Order order = new Order();
            var checkedItemsJson = TempData["checkedItems"] as string;
            var quantitiesJson = TempData["quantities"] as string;
            var clientJson = TempData["client"] as string;

            var checkedItems = JsonSerializer.Deserialize<List<Item>>(checkedItemsJson);
            var quantities = JsonSerializer.Deserialize<List<Quantity>>(quantitiesJson);
            var client = JsonSerializer.Deserialize<Client>(clientJson);
            _context.Client.Attach(client);

            List<Item> updatedItems = new List<Item>();
            foreach (var item in checkedItems)
            {
                var itemToUpdate = _context.Items.Find(item.Id);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Quantity -= 1;
                    updatedItems.Add(itemToUpdate);
                }
            }

            //_context.SaveChanges();

            order.Items = updatedItems;
            order.Quantity = quantities;
            order.OrderClient = client;
            order.TrackingNumber = RandomString(8);
            order.OrderDate = DateTime.Now;
            order.ShippingDate = DateTime.Now.AddDays(1);
            order.ArrivalDate = DateTime.Now.AddDays(2);

            _context.Add(order);
            _context.SaveChanges();

            return Redirect("Index");
        }

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,TrackingNumber,OrderDate,ShippingDate,ArrivalDate")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackingNumber,OrderDate,ShippingDate,ArrivalDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'Retail_Data_TrackerContext.Orders'  is null.");
            }
            var order = _context.Orders.Include(o => o.Quantity).Include(o => o.Items).FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _context.Items.RemoveRange(order.Items);
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
