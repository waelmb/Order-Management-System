using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace WebApplication3.Pages
{  
    public class getServiceModel : PageModel  
    {  
	    public string serviceName { get; set; }

        public string serviceDescription { get; set; }

        public int servicePrice { get; set; }

        public string input { get; set; }

        public string message = "";


        public void OnGet()  
        {  
				// no data needed for initial view:
	    }

     
        public void OnGetError()
        {
            message = "Error: an error occured.";
        }

    }//class  
}//namespace