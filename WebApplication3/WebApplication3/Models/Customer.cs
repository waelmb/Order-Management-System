using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Customer
    {
        public string email;
        public string firstName;
        public string middleName;
        public string lastName;
        public string phone;


        public Customer()
        {
            email = "null";
            firstName = "null";
            middleName = "null";
            lastName = "null";
            phone = "null";
        }

        public Customer(string _email, string _firstName, string _middleName, string _lastName, string _phone)
        {
            email = _email;
            firstName = _firstName;
            middleName = _middleName;
            lastName = _lastName;
            phone = _phone;
        }
    }
}
