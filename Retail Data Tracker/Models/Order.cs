using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Item> Items {get; set;}
        [NotMapped]
        public List<int> Quantity {get; set;}
        public string TrackingNumber { get; set; }
        public string OrderDate { get; set; }
        public string ShippingDate { get; set; }
        public string ArrivalDate { get; set; }
        public Client OrderClient {get; set;}
    }
}