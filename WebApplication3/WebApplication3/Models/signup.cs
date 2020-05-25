using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace WebApplication3.Pages
{  
    public class SignupModel : PageModel  
    {  
	    public string _email { get; set; }

        public string _password { get; set; }

        public string _firstName { get; set; }
        public string _middleName { get; set; }
        public string _lastName { get; set; }
        
        public string _phone { get; set; }

        public string message = "";


        public void OnGet()  
        {

        }

        public void OnGetWrongInput()
        {
            message = "Error: username already exists.";
        }

        public void OnGetError()
        {
            message = "Error: an error occured.";
        }

    }//class  
}//namespace