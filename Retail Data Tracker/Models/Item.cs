using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Retail_Data_Tracker.Models
{
    public class Item
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 255 characters")]
        public string Name { get; set; }
        
        [StringLength(1000, ErrorMessage = "Item description cannot exceed 1000 characters")]
        public string ItemDesc { get; set; }
        
        public int Quantity { get; set; }
        public int QuantityWanted { get; set; }
        public int SupplierId { get; set; }
        
        [Required(ErrorMessage = "Buy cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Buy cost must be greater than 0")]
        public double BuyCost { get; set;}

        [Required(ErrorMessage = "Sell cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sell cost must be greater than 0")]
        public double SellCost {get; set;}
        public bool IsChecked { get; set; }
        [BindNever]
        public Supplier ItemSupplier { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}