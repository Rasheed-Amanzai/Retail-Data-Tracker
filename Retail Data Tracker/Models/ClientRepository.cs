using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Data_Tracker.Models
{
    public class ClientRepository
    {
        private static List<Client> _clientList = new List<Client>();

        public static IEnumerable<Client> Clients {
            get {
                return _clientList;
            }
        }
    }
}