using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class SupplierRepository
    {
        private static List<Supplier> _supplierList = new List<Supplier>();

        public static IEnumerable<Supplier> Suppliers {
            get
            {
                return _supplierList;
            }
        }

        public static void AddSupplier(Supplier supplier)
        {
            _supplierList.Add(supplier);
            // TODO: Add the new supplier to the database
        }

        public static void RemoveSupplier(Supplier supplier)
        {
            _supplierList.Remove(supplier);
            // TODO: Remove the supplier entry from the database
        }
    }
}
