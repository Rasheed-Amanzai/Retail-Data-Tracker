using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Order
    {
        public List<Item> Items {get; set;}
        public List<int> Quantity {get; set;}
        public Shipping ShippingDetails {get; set;}
        public Client OrderClient {get; set;}
    }
}