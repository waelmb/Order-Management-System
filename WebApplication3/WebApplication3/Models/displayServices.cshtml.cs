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
	public class displayServicesModel : PageModel
	{

		public List<Services> services { get; set; }

		public Exception EX { get; set; }


		public void OnGet()
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
	}
}
