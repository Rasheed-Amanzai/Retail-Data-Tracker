using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class Shipping
    {
        public int Id { get; set;}
        public int OrderNumber {get; set;}
        public Client client {get; set;}
    }
}