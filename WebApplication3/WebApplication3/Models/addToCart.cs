using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Pages
{
	public class addToCartModel : PageModel
	{
		public int InputServiceID;
		public int InputRequest;
		public List<Services> services { get; set; }
		public List<OrderItem> orderItems { get; set; }
		public int totalPrice = 0;

		public Exception EX { get; set; }

	

		public IActionResult OnGet(int input, int inputId)
		{
			Console.WriteLine("**** in OG onGet");
			// make input available to web page:
			InputRequest = input;
			InputServiceID = inputId;
			Console.WriteLine("input: " + input);
			Console.WriteLine("inputId: " + inputId);
			services = new List<Services>();
			orderItems = new List<OrderItem>();

			// clear exception:
			EX = null;

			try
			{

				if (HttpContext.Session.GetString("firstName") == null || HttpContext.Session.GetString("firstName").Equals("null"))
				{
					return new RedirectToPageResult("getLogin", "LoginFirst");
				}
				else
				{
					Console.WriteLine("**** deleting/adding");
					var user_id = HttpContext.Session.GetInt32("user_id");
					//delete request
					if (input == 2)
					{
						//delete service to cart table in database
						string delCmd = string.Format(@"DELETE FROM `ordermanagementdb`.`cart` WHERE (`service_id` = '{0}') and (`user_id` = '{1}');", InputServiceID, user_id);
						DataAccessTier.DB.ExecuteActionQuery(delCmd);

					}
					//add request
					else if (input == 1)
					{

						//check if service is already in the cart
						string checkCmd = string.Format(@"SELECT * FROM ordermanagementdb.cart WHERE service_id = {0} and (`user_id` = '{1}'); ", InputServiceID, user_id);
						DataSet dsCart = DataAccessTier.DB.ExecuteNonScalarQuery(checkCmd);

						if (dsCart.Tables[0].Rows.Count <= 0)
						{
							//add service to cart table in database
							string addCmd = string.Format(@"INSERT INTO `ordermanagementdb`.`cart` (`service_id`, `quantity`, `user_id`) VALUES ('{0}', '1', '{1}');", InputServiceID, user_id);


							DataAccessTier.DB.ExecuteActionQuery(addCmd);

						}
						else
						{
							//add 1 to quantity of cart item
							int q = 1;

							foreach (DataRow row in dsCart.Tables[0].Rows)
							{
								q = q + Convert.ToInt32(row["quantity"]);
							}

							string addCmd = string.Format(@"UPDATE `ordermanagementdb`.`cart` SET `quantity` = '{0}' WHERE (`service_id` = '{1}') and (`user_id` = '{2}');", q, InputServiceID, user_id);

							DataAccessTier.DB.ExecuteActionQuery(addCmd);

						}
					}//else
				}
			}
			catch (Exception ex)
			{
				EX = ex;
			}
			finally
			{
				
			}
			return new RedirectToPageResult("addToCart", "Display");
		}

		public void OnGetDisplay()
		{
			Console.WriteLine("**** in Display onGet");
			services = new List<Services>();
			orderItems = new List<OrderItem>();

			if (HttpContext.Session.GetString("firstName") != null && !HttpContext.Session.GetString("firstName").Equals("null"))
			{
				var user_id = HttpContext.Session.GetInt32("user_id");
				//retrieve all items in shopping cart
				string sql = string.Format(@"SELECT * FROM services INNER JOIN cart ON services.service_id = cart.service_id WHERE (`user_id` = '{0}'); ", user_id);

				DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);


				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Services S = new Services(Convert.ToInt32(row["service_id"]), Convert.ToString(row["service_name"]), Convert.ToString(row["service_description"]), Convert.ToInt32(row["service_price"]));
					OrderItem item = new OrderItem(S, Convert.ToInt32(row["quantity"]));
					totalPrice = totalPrice + (Convert.ToInt32(row["quantity"]) * Convert.ToInt32(row["service_price"]));
					orderItems.Add(item);
				}
			}
		}
	}
}
