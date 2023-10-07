using Retail_Data_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Retail_Data_Tracker.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult OrderForm(Order order)
        {
            // TODO: Handle Order form submission when user wants to create a new invoice/order,
            // and if successful, redirect the user to a confirmation page (or a different page)
            return View(order);
        } 
    }
}