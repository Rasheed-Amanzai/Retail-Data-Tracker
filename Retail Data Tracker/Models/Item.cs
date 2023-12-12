using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public int QuantityWanted { get; set; }
        public int SupplierId { get; set; }

        public double BuyCost { get; set;}
        public double SellCost {get; set;}
        public bool IsChecked { get; set; }
        public Supplier ItemSupplier { get; set; }

        public Supplier GetSupplier()
        {
            return ItemSupplier;
        }

        public void SetSupplier(Supplier supplier)
        {
            ItemSupplier = SupplierRepository.Suppliers.FirstOrDefault(s => s.Id == SupplierId);
        }
    }
}