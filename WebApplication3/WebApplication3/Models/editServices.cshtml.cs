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
	public class editServicesModel : PageModel
	{

		public List<Services> services { get; set; }

		public string message = "";

		public int Action { get; set; }

		public int ServiceId { get; set; }

		public Exception EX { get; set; }


		public IActionResult OnGet(int input, int serviceId)
		{
			// make input available to web page:
			services = new List<Services>();
			Action = input;
			ServiceId = serviceId;
			Console.WriteLine("**** _action: " + input);
			Console.WriteLine("**** _serviceId: " + serviceId);


			// clear exception:
			EX = null;

			try
			{


				//delete request
				if (input == 1)
				{
					string delCmd = string.Format(@"DELETE FROM `ordermanagementdb`.`services` WHERE (`service_id` = '{0}');", serviceId);
					DataAccessTier.DB.ExecuteActionQuery(delCmd);
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

			return new RedirectToPageResult("editServices", "Display");
		}

		public void OnGetDisplay()
		{
			// make input available to web page:
			services = new List<Services>();


			// clear exception:
			EX = null;

			try
			{


				string sql = "SELECT * FROM services";

				DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
					services.Add(S);
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

		public void OnGetError()
		{
			message = "An error occured.";

			// make input available to web page:
			services = new List<Services>();


			// clear exception:
			EX = null;

			try
			{


				string sql = "SELECT * FROM services";

				DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
					services.Add(S);
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
