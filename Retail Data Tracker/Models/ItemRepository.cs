using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This is effectively the Inventory of the store

namespace Retail_Data_Tracker.Models
{
    public class ItemRepository
    {
        
        private static List<Item> responses = new List<Item>();

        public static IEnumerable<Item> Items {
            get {
                return responses;
            }
        } 

        public static void AddResponse(Item item){
            responses.Add(item);
        }

    }
}