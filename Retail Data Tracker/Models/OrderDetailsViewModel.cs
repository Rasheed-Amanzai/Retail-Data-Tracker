/// Written by Matthew-Nocera-Iozzo (GitHub: @afk-m) ///

using Retail_Data_Tracker.Models;

namespace Retail_Data_Tracker.Models;

public class OrderDetailsViewModel
{
    public List<ItemQuantityViewModel> ItemQuantities { get; set; } = new List<ItemQuantityViewModel>();
    public double OrderTotal { get; set; }
    public OrderType OrderType { get; set; }

    public OrderDetailsViewModel(Order order)
    {
        // Assuming 'order' is an instance of your Order class with OrderItems populated
        foreach (var orderItem in order.OrderItems)
        {
            ItemQuantities.Add(new ItemQuantityViewModel
            {
                Item = orderItem.Item,
                Quantity = orderItem.QuantityNumber
            });
        }

        OrderTotal = order.OrderTotal;
        OrderType = order.OrderType;
    }
}