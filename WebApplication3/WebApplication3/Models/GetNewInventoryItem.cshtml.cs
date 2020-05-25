using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace WebApplication3.Pages
{  
    public class GetNewInventoryItemModel : PageModel  
    {  
	    public string Input { get; set; }

        public string Input2 { get; set; }

        public void OnGet()  
      {  
				// no data needed for initial view:
		  }
			
    }//class  
}//namespace