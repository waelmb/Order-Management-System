using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using DataBaseAccess;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Pages
{
	public class SignupProcessModel : PageModel
	{

		public User user { get; set; }
		
		public string message { get; set; }

		public Exception EX { get; set; }

		public IActionResult OnGet(string _email, string _password, string _firstName, string _middleName, string _lastName, string _phone)
		{
			user = new User();
			Console.WriteLine("_email: " + _email);
			Console.WriteLine("_password: " + _password);
			Console.WriteLine("_firstName: " + _firstName);
			Console.WriteLine("_middleName: " + _middleName);
			Console.WriteLine("_lastName: " + _lastName);
			Console.WriteLine("_phone: " + _phone);


			// clear exception:
			EX = null;

			try
			{
				//
				// Do we have an input argument?  If not, there's nothing to do:
				//
				if (_email == null || _password == null || _firstName == null || _lastName == null || _phone == null)
				{
					message = "Error: username and password weren't entered correctly.";
					return new RedirectToPageResult("signup", "Error");
				}
				else
				{

					string sql = string.Format(@"SELECT * FROM ordermanagementdb.users WHERE email = '{0}';", _email);

					DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

					//check if there is more than 0 entry in the dataset
					if(ds.Tables[0].Rows.Count <= 0)
					{
						//create user, customer and session info
						int userId = generateUserId();
						user = new User(userId, _email, _password, "customer", _firstName, _middleName, _lastName, _phone);
						message = string.Format(@"Welcome, {0}.", user.firstName);
						HttpContext.Session.SetInt32("user_id", user.userId);
						HttpContext.Session.SetString("firstName", user.firstName);
						HttpContext.Session.SetString("role", user.role);

						//insert to users
						string insertUserCmd = string.Format(@"INSERT INTO `ordermanagementdb`.`users` (`user_id`, `email`, `password`, `role`, `first_name`, `middle_name`, `last_name`, `phone`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');", user.userId, user.email, user.password, user.role, user.firstName, user.middleName, user.lastName, user.phone);
						DataAccessTier.DB.ExecuteActionQuery(insertUserCmd);


						return new RedirectToPageResult("Index");
					}
					else
					{
						message = "Error: username already exists.";
						return new RedirectToPageResult("signup", "WrongInput");

					}

				}//else
			}
			catch (Exception ex)
			{
				EX = ex;
			}
			finally
			{
				// nothing at the moment
			}
			return new RedirectToPageResult("signup", "Error");
		}

		//generates an order number sequentially
		public int generateUserId()
		{
			//select everything in order table

			string sql = "SELECT user_id FROM users";

			DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

			//return the number of count
			return ds.Tables[0].Rows.Count;
		}
	}
}
