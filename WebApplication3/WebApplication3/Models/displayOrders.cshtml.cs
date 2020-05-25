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
	public class displayOrdersModel : PageModel
	{

		public List<OrderObject> orders { get; set; }

		public Exception EX { get; set; }


		public void OnGet()
		{
			// make input available to web page:
			orders = new List<OrderObject>();

			// clear exception:
			EX = null;

			try
			{
				//retreive all orders 
				
				string sql = "SELECT * FROM ordermanagementdb.orders;";

				DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					OrderObject o = new OrderObject();
					o.orderId = Convert.ToInt32(row["order_id"]);
					o.userId = Convert.ToInt32(row["user_id"]);
					orders.Add(o);
				}

				//for each order, get items 
				foreach(OrderObject order in orders)
				{
					//retrieve all items of the order
					string sqlOrder = string.Format("SELECT * FROM services INNER JOIN order_items ON services.service_id = order_items.service_id WHERE order_id={0};", order.orderId);

					DataSet dsOrder = DataAccessTier.DB.ExecuteNonScalarQuery(sqlOrder);


					foreach (DataRow row in dsOrder.Tables[0].Rows)
					{
						Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
						OrderItem item = new OrderItem(S, Convert.ToInt32(row["quantity"]));
						order.total = order.total + (Convert.ToInt32(row["quantity"]) * Convert.ToInt32(row["service_price"]));
						order.items.Add(item);
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

				//reverse list to give most recent orders
				orders.Reverse();

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
