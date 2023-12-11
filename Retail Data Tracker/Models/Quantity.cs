using System.ComponentModel.DataAnnotations;

namespace Retail_Data_Tracker.Models
{
    public class Quantity
    {
        [Key]
        public int Id { get; set; }
        public int QuantityNumber { get; set; }
    }
}
