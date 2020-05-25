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
	public class displayOneOrderModel : PageModel
	{

		public OrderObject order { get; set; }

		public Exception EX { get; set; }


		public void OnGet(int orderId)
		{
			// make input available to web page:
			order = new OrderObject();
			Console.WriteLine(orderId);

			// clear exception:
			EX = null;

			try
			{
				if(orderId >= 0)
				{
					//retreive order info 

					string sqlInfo = string.Format("SELECT * FROM ordermanagementdb.orders WHERE order_id = '{0}';", orderId);

					DataSet dsInfo = DataAccessTier.DB.ExecuteNonScalarQuery(sqlInfo);

					foreach (DataRow row in dsInfo.Tables[0].Rows)
					{
					    order.orderId = Convert.ToInt32(row["order_id"]);
						order.userId = Convert.ToInt32(row["user_id"]);
					}

					//retrieve all items of the order
					string sql = string.Format("SELECT * FROM services INNER JOIN order_items ON services.service_id = order_items.service_id WHERE order_id={0};", order.orderId);

					DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

					foreach (DataRow row in ds.Tables[0].Rows)
					{
						Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
						OrderItem item = new OrderItem(S, Convert.ToInt32(row["quantity"]));
						order.total = order.total + (Convert.ToInt32(row["quantity"]) * Convert.ToInt32(row["service_price"]));
						order.items.Add(item);
					}
				}

				//retrieve customer info
				string sqlCustomer = string.Format(@"SELECT email, first_name, middle_name, last_name, phone FROM ordermanagementdb.users WHERE user_id = '{0}';", order.userId);

				DataSet dsCustomer = DataAccessTier.DB.ExecuteNonScalarQuery(sqlCustomer);

				foreach (DataRow row in dsCustomer.Tables[0].Rows)
				{
					order.fullName = Convert.ToString(row["first_name"]) + " " + Convert.ToString(row["middle_name"]) + " " + Convert.ToString(row["last_name"]);
					order.email = Convert.ToString(row["email"]);
					order.phone = Convert.ToString(row["phone"]);
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
