﻿using Retail_Data_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Retail_Data_Tracker.Data;
using Microsoft.IdentityModel.Tokens;

namespace Retail_Data_Tracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderForm(Order order)
        {
            // TODO: Handle Order form submission when user wants to create a new invoice/order,
            // and if successful, redirect the user to a confirmation page (or a different page)
            return View(order);
        } 

        [HttpPost]
        public ActionResult Inventory(string CreateInvoice, Order order, Retail_Data_TrackerContext retailContext)
        {
            foreach(var item in retailContext.Items){
                if (item.IsChecked){
                    order.Items.Add(item);
                } 
            }
            if (order.Items.IsNullOrEmpty())
            {
                return RedirectToAction("OrderForm", order);
            }
            else
            {
                return RedirectToAction("OrderForm", order);
            }
        }
    }
}