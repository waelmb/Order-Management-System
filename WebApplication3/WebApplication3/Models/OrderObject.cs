using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class OrderObject
    {
        public int userId;
        public string fullName;
        public string email;
        public string phone;
        public int orderId;
        public List<OrderItem> items;
        public int total;
  
        public OrderObject()
        {
            userId = -1;
            orderId = -1;
            email = "null";
            fullName = "null";
            items = new List<OrderItem>();
            phone = "null";
            total = 0;
        }

        public OrderObject(int id, string _email, string _fullName, string _phone, int _orderId, List<OrderItem> _items, int _total)
        {
            userId = id;
            orderId = _orderId;
            email = _email;
            fullName = _fullName;
            items = _items;
            phone = _phone;
            total = _total;
        }
    }
}
