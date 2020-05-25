using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Pages
{  
    public class SignoutModel : PageModel  
    {  
	    public string _email { get; set; }

        public string _password { get; set; }

        public string message = "";


        public IActionResult OnGet()  
         {
            HttpContext.Session.SetInt32("user_id", -1);
            HttpContext.Session.SetString("firstName", "null");
            HttpContext.Session.SetString("role", "null");
            return new RedirectToPageResult("Index");
        }
     

    }//class  
}//namespace