using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Retail_Data_Tracker.Models
{

    public class ItemCreateViewModel
    {
        public string Name { get; set; }
        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public double BuyCost { get; set; }
        public double SellCost { get; set; }


        // Property to hold the list of suppliers
        public List<Supplier> Suppliers { get; set; }
    }
}