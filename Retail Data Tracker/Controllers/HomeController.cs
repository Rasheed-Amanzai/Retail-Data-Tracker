using Retail_Data_Tracker.Models;
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

        public ActionResult ClientsView()
        {
            return RedirectToAction("Index", "Clients");
        }

        public ActionResult ItemsView()
        {
            return RedirectToAction("Index", "Items");
        }

        public ActionResult OrdersView()
        {
            return RedirectToAction("Index", "Orders");
        }

        public ActionResult SuppliersView()
        {
            return RedirectToAction("Index", "Suppliers");
        }

        //[HttpPost]
        //public ActionResult Inventory(string CreateInvoice, Order order, Retail_Data_TrackerContext retailContext)
        //{
        //    foreach(var item in retailContext.Items){
        //        if (item.IsChecked){
        //            order.OrderItems.Add(item);
        //        } 
        //    }
        //    if (order.Items.IsNullOrEmpty())
        //    {
        //        return RedirectToAction("OrderForm", order);
        //    }
        //    else
        //    {
        //        return RedirectToAction("OrderForm", order);
        //    }
        //}
    }
}