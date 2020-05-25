using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Services
    {
        public int serviceId;
        public string serviceName;
        public string serviceDescription;
        public int servicePrice;

        public Services(int id, string name, string description, int price)
        {
            serviceId = id;
            serviceName = name;
            serviceDescription = description;
            servicePrice = price;
        }
    }
}
