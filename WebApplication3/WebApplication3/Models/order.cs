using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApplication3.Models;
using System.IO;
using IronPdf;
using Microsoft.AspNetCore.Http;


namespace WebApplication3.Pages
{
	public class orderModel : PageModel
	{
		public int OrderId = -1;
		public int InputRequest;
		public int InputId;
		public List<Services> services { get; set; }
		public List<OrderItem> orderItems { get; set; }
		public int totalPrice = 0;

		public Exception EX { get; set; }

	

		public void OnGet(int input, int inputId)
		{
			// make input available to web page:
			InputRequest = input;
			InputId = inputId;
			Console.WriteLine("*****input: " + input);
			Console.WriteLine("*****inputId: " + inputId);
			services = new List<Services>();
			orderItems = new List<OrderItem>();

			// clear exception:
			EX = null;

			try
			{
				//
				// Do we have an input argument?  If not, there's nothing to do:
				//
				//add request
				if (input == 1)
				{
					var user_id = HttpContext.Session.GetInt32("user_id");
					//retrieve all items in shopping cart for the user
					string sqldsCart = string.Format(@"SELECT * FROM services INNER JOIN cart ON services.service_id = cart.service_id WHERE user_id = '{0}'; ", user_id);

					DataSet dsCart = DataAccessTier.DB.ExecuteNonScalarQuery(sqldsCart);

					foreach (DataRow row in dsCart.Tables[0].Rows)
					{
						Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
						OrderItem item = new OrderItem(S, Convert.ToInt32(row["quantity"]));
						totalPrice = totalPrice + (Convert.ToInt32(row["quantity"]) * Convert.ToInt32(row["service_price"]));
						orderItems.Add(item);
					}

					//generate order id, and insert to orders table
					int orderId = generateOrderId();
					OrderId = orderId;
					Console.WriteLine("*****orderId: " + orderId);
					string addOrderCmd = string.Format(@"INSERT INTO `ordermanagementdb`.`orders` (`order_id`, `user_id`) VALUES ('{0}', '{1}');", orderId, user_id);
					DataAccessTier.DB.ExecuteActionQuery(addOrderCmd);

					//insert order items into order_items table
					foreach (OrderItem item in orderItems)
					{
						string addOrderItemsCmd = string.Format(@"INSERT INTO `ordermanagementdb`.`order_items` (`order_id`, `service_id`, `quantity`) VALUES ('{0}', '{1}', '{2}');", orderId, item.service.serviceId, item.quantity );
						DataAccessTier.DB.ExecuteActionQuery(addOrderItemsCmd);
					}

					//delete everything in shopping cart
					string delCartCmd = string.Format(@"DELETE FROM cart WHERE user_id = '{0}';", user_id);
					DataAccessTier.DB.ExecuteActionQuery(delCartCmd);

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

		//generates an order number sequentially
		public int generateOrderId()
		{
			//select everything in order table

			string sql = "SELECT order_id FROM orders";

			DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

			//return the number of count
			return ds.Tables[0].Rows.Count;
		}

		//print order handler
		public void OnGetReceipt(int inputID)
		{
			orderItems = new List<OrderItem>();
			OrderId = inputID;
			string html = string.Format("<h1>Thank you for your order!</h1> <b>Order #:{0}</b><table><thead><tr><th>Service Name</th><th>Quantity</th><th>Service Price</th></tr></thead><tbody>", OrderId);
			string fileName = string.Format("receipt{0}.pdf", OrderId);

			//retrieve all items of the order
			string sql = string.Format("SELECT * FROM services INNER JOIN order_items ON services.service_id = order_items.service_id WHERE order_id={0};", OrderId);

			DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);


			foreach (DataRow row in ds.Tables[0].Rows)
			{
				Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
				OrderItem item = new OrderItem(S, Convert.ToInt32(row["quantity"]));
				totalPrice = totalPrice + (Convert.ToInt32(row["quantity"]) * Convert.ToInt32(row["service_price"]));
				orderItems.Add(item);

				//add to html
				string line = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>;", Convert.ToString(row["service_name"]), Convert.ToInt32(row["quantity"]), Convert.ToInt32(row["service_price"]));
				html += line;
			}

			string footer = string.Format("</tbody></table><b>Total Price: {0} rub.</b>", totalPrice);
			html += footer;

			//render pdf
			var Renderer = new IronPdf.HtmlToPdf();
			Renderer.RenderHtmlAsPdf(html).SaveAs(fileName);
		}
	}
}
