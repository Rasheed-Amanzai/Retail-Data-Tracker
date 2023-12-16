using System.ComponentModel.DataAnnotations;

namespace Retail_Data_Tracker.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Key]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int QuantityNumber { get; set; } // Assuming this is the quantity for this specific item in the order
    }
}
