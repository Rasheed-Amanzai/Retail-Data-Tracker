using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }

        public double BuyCost { get; set;}
        public double SellCost {get; set;}
    }
}