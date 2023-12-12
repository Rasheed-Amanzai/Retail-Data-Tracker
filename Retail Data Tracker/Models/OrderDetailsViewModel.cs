using Retail_Data_Tracker.Models;

namespace Retail_Data_Tracker.Models;

public class OrderDetailsViewModel
{
    public List<ItemQuantityViewModel> ItemQuantities { get; set; }
    public double OrderTotal { get; set; }
}