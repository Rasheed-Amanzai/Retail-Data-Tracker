using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Shipping
    {
        public int OrderNumber {get; set;}
        public string TrackingNumber {get; set;}
        public string OrderDate {get; set;}
        public string ShippingDate {get; set;}
        public string ArrivalDate {get; set;}
        public Client client {get; set;}
    }
}