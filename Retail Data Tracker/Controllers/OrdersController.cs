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
            var orders = _context.Orders.Include(o => o.OrderClient)
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Item)
    .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine(order.OrderTotal.ToString("C2"));
            }

            return orders != null ?
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

            var order = _context.Orders
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Item) // Include the related Item entities within OrderItems
    .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailsViewModel(order);


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
            var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
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
