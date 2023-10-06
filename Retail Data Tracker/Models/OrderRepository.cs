using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class OrderRepository
    {
        private static List<Order> responses = new List<Order>();

        public static IEnumerable<Order> Orders {
            get {
                return responses;
            }
        }

        public static void AddResponse(Order order){
            responses.Add(order);
        }
    }
}