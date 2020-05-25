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
	public class LoginModel : PageModel
	{

		public User user { get; set; }
		
		public string message { get; set; }

		public Exception EX { get; set; }

		public IActionResult OnGet(string _email, string _password)
		{
			user = new User();
			Console.WriteLine("_email: " + _email);
			Console.WriteLine("_password: " + _password);

			// clear exception:
			EX = null;

			try
			{
				//
				// Do we have an input argument?  If not, there's nothing to do:
				//
				if (_email == null || _password == null)
				{
					message = "Error: username and password weren't entered correctly.";
					return new RedirectToPageResult("getLogin", "WrongInput");
				}
				else
				{

					string sql = string.Format(@"SELECT * FROM ordermanagementdb.users WHERE email = '{0}';", _email);

					DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

					//check if there is exactly 1 entry in the dataset
					if(ds.Tables[0].Rows.Count == 1)
					{
						foreach (DataRow row in ds.Tables[0].Rows)
						{
							int dsUserId = Convert.ToInt32(row["user_id"]);
							string dsEmail = Convert.ToString(row["email"]);
							string dsPassword = Convert.ToString(row["password"]);
							string dsRole = Convert.ToString(row["role"]);
							string dsFirstName = Convert.ToString(row["first_name"]);
							string dsMiddleName = Convert.ToString(row["middle_name"]);
							string dsLastName = Convert.ToString(row["last_name"]);
							string dsPhone = Convert.ToString(row["phone"]);

							if (dsEmail.Equals(_email) && dsPassword.Equals(_password))
							{
								user = new User(dsUserId, dsEmail, dsPassword, dsRole, dsFirstName, dsMiddleName, dsLastName, dsPhone);
								message = string.Format(@"Welcome, {0}.", dsFirstName);
								HttpContext.Session.SetInt32("user_id", dsUserId);
								HttpContext.Session.SetString("firstName", dsFirstName);
								HttpContext.Session.SetString("role", dsRole);

								return new RedirectToPageResult("Index");
							}
							else
							{
								message = "Error: username or password combination do not match our records.";
								return new RedirectToPageResult("getLogin", "WrongCredentials");
							}
						}
					}
					else
					{
						message = "Error: username or password combination do not match our records.";
						return new RedirectToPageResult("getLogin", "WrongCredentials");

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
			return new RedirectToPageResult("getLogin", "Error");
		}
	}
}
