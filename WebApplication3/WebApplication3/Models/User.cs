using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class User
    {
        public int userId;
        public string email;
        public string password;
        public string role;
        public string firstName;
        public string middleName;
        public string lastName;
        public string phone;


        public User()
        {
            userId = -1;
            email = "null";
            password = "null";
            role = "null";
            firstName = "null";
            middleName = "null";
            lastName = "null";
            phone = "null";
        }

        public User(int id, string _email, string _password, string r, string _firstName, string _middleName, string _lastName, string _phone)
        {
            userId = id;
            email = _email;
            password = _password;
            role = r;
            firstName = _firstName;
            middleName = _middleName;
            lastName = _lastName;
            phone = _phone;
        }
    }
}
