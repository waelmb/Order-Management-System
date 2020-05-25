using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace WebApplication3.Pages
{  
    public class getLoginModel : PageModel  
    {  
	    public string _email { get; set; }

        public string _password { get; set; }

        public string message = "";


        public void OnGet()  
      {  
				// no data needed for initial view:
	  }

        public void OnGetWrongCredentials()
        {
            message = "Error: email or password combination do not match our records.";
        }

        public void OnGetWrongInput()
        {
            message = "Error: email and password weren't entered correctly.";
        }

        public void OnGetLoginFirst()
        {
            message = "Error: please login first.";
        }

        public void OnGetError()
        {
            message = "Error: an error occured.";
        }

    }//class  
}//namespace