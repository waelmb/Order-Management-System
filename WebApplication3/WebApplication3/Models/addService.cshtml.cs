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
	public class addServiceModel : PageModel
	{


		public string message = "";

		public Exception EX { get; set; }


		public IActionResult OnGet(string serviceName, string serviceDescription, int servicePrice)
		{
			Console.WriteLine("**** _serviceName: " + serviceName);
			Console.WriteLine("**** _serviceDescription: " + serviceDescription);
			Console.WriteLine("**** _servicePrice: " + servicePrice);


			// clear exception:
			EX = null;

			try
			{
				if(serviceName == null || serviceDescription == null)
				{
					return new RedirectToPageResult("editServices", "Error");
				}
				else
				{
					int serviceId = generateServiceId();
					Console.WriteLine("**** serviceId: " + serviceId);
					string insertServiceCmd = string.Format(@"INSERT INTO `ordermanagementdb`.`services` (`service_id`, `service_name`, `service_description`, `service_price`) VALUES ('{0}', '{1}', '{2}', '{3}');", serviceId, serviceName, serviceDescription, servicePrice);
					DataAccessTier.DB.ExecuteActionQuery(insertServiceCmd);
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

		public int generateServiceId()
		{
			//select everything in services table

			string sql = "SELECT service_id FROM services";

			DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

			//return the number of count
			return ds.Tables[0].Rows.Count;
		}
	}
}
