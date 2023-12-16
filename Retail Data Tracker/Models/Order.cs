using Retail_Data_Tracker.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public enum OrderType { Retailer, Supplier }

    public class Order
    {
        public int Id { get; set; }
        //public List<Item> Items { get; set; } = new List<Item>();
        //public List<Quantity> Quantity {get; set;} = new List<Quantity>();
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
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderType OrderType { get; set; }

        public double OrderTotal
        {
            get
            {
                double orderTotalNumber = 0;
                if (OrderType == OrderType.Retailer)
                {
                    foreach (var orderItem in OrderItems)
                    {
                        if (orderItem.Item != null)
                        {
                            orderTotalNumber += orderItem.Item.SellCost * orderItem.QuantityNumber;
                        }
                    }
                } else if (OrderType == OrderType.Supplier)
                {
                    foreach (var orderItem in OrderItems)
                    {
                        if (orderItem.Item != null)
                        {
                            orderTotalNumber += orderItem.Item.BuyCost * orderItem.QuantityNumber;
                        }
                    }
                }
                return orderTotalNumber;
            }
        }
    }
}