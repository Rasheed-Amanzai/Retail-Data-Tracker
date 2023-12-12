using Retail_Data_Tracker.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{

    public class Order
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Quantity> Quantity {get; set;} = new List<Quantity>();
        public string TrackingNumber { get; set; }
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        public DateTime ShippingDate { get; set; }
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        public Client OrderClient { get; set; } = new Client();
        public double OrderTotal 
        {
            get
            {
                double OrderTotalNumber = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    OrderTotalNumber += (Items[i].SellCost * Quantity[i].QuantityNumber);
                }
                return OrderTotalNumber;
            }
        }
    }
}