using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using DataBaseAccess;

namespace WebApplication3.Pages
{
	public class displayInventoryModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public string InputName { get; set; }
		public decimal InputPrice { get; set; }
		public List<string> firstNames { get; set; }

		public Exception EX { get; set; }

		public displayInventoryModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet(string input, string input2)
		{
			// make input available to web page:
			InputName = input;
			InputPrice = decimal.Parse(input2);
			firstNames = new List<string>();


			// clear exception:
			EX = null;

			try
			{
				//
				// Do we have an input argument?  If not, there's nothing to do:
				//
				if (input == null || input2 == null)
				{
					//
					// there's no page argument, perhaps user surfed to the page directly?  
					// In this case, nothing to do.
					//
				}
				else
				{
					//string sql = "SELECT first_name FROM customers ORDER BY first_name";
					//string sql = "SELECT Name FROM Inventory";
					string sql = "SELECT service_name FROM services";

					DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
					//DataSet ds = DataBaseAccess.DataBaseAdaptor.ExecuteNonScalarQuery(sql);

					foreach (DataRow row in ds.Tables[0].Rows)
					{
						firstNames.Add(Convert.ToString(row["service_name"]));
					}

					//DataBaseAccess.DataBaseAdaptor.insertData();

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
		}
	}
}
