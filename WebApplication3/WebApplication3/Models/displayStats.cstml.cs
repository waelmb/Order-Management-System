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
	public class displayStatsModel : PageModel
	{

		public List<OrderItem> orderItems { get; set; }

		public List<string> Services { get; set; }

		public List<int> Quantities { get; set; }

		public Exception EX { get; set; }


		public void OnGet()
		{
			// make input available to web page:
			orderItems = new List<OrderItem>();
			Services = new List<string>();
			Quantities = new List<int>();

			// clear exception:
			EX = null;

			try
			{
				//get a list of service_ids
				List<Services> services = new List<Services>();

				string sqlServiceIds = "SELECT * FROM ordermanagementdb.services;";

				DataSet dsServiceIds = DataAccessTier.DB.ExecuteNonScalarQuery(sqlServiceIds);



				foreach (DataRow row in dsServiceIds.Tables[0].Rows)
				{
					Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
					services.Add(S);
				}

				//for each service id, extract the number of occurences in the database
				foreach(Services S in services)
				{
					OrderItem orderItem = new OrderItem(S, 0);

					string sqlQuan = string.Format(@"SELECT quantity FROM ordermanagementdb.order_items WHERE service_id = {0};", S.serviceId);

					DataSet dsQuan = DataAccessTier.DB.ExecuteNonScalarQuery(sqlQuan);

					foreach (DataRow row in dsQuan.Tables[0].Rows)
					{
						orderItem.quantity += Convert.ToInt32(row["quantity"]);
					}

					orderItems.Add(orderItem);

				}

				//fill out Services and Quantities
				foreach(OrderItem item in orderItems)
				{
					Services.Add(item.service.serviceName);
					Quantities.Add(item.quantity);
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
