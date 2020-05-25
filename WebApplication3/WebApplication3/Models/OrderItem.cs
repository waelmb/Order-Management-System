using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class OrderItem
    {
        public Services service;
        public int quantity;

        public OrderItem(Services s, int q)
        {
            service = s;
            quantity = q;
        }
    }
}
