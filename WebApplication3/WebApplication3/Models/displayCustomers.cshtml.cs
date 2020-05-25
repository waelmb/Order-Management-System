using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApplication3.Models;

namespace WebApplication3.Pages
{
	public class displayCustomersModel : PageModel
	{

		public List<Customer> customers { get; set; }

		public Exception EX { get; set; }


		public void OnGet()
		{
			// make input available to web page:
			customers = new List<Customer>();


			// clear exception:
			EX = null;

			try
			{
				string sql = "SELECT email, first_name, middle_name, last_name, phone FROM ordermanagementdb.users WHERE role = 'customer';";

				DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Customer c = new Customer(Convert.ToString(row["email"]), Convert.ToString(row["first_name"]), Convert.ToString(row["middle_name"]), Convert.ToString(row["last_name"]), Convert.ToString(row["phone"]));
					customers.Add(c);
				}

			}
			catch (Exception ex)
			{
				EX = ex;
			}
			finally
			{
				// nothing at the moment
			}
		}
	}
}
