#pragma checksum "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50ec223b6001aea44bf17af7f80a0a1b10878d9a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebApplication3.Pages.Pages_displayInventory), @"mvc.1.0.razor-page", @"/Pages/displayInventory.cshtml")]
namespace WebApplication3.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\_ViewImports.cshtml"
using WebApplication3;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50ec223b6001aea44bf17af7f80a0a1b10878d9a", @"/Pages/displayInventory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3c0aa44830bef36841a062868044c1fc5e79428b", @"/Pages/_ViewImports.cshtml")]
    public class Pages_displayInventory : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
  
    ViewData["Title"] = "Inventory";
    var firstNames = Newtonsoft.Json.JsonConvert.SerializeObject(Model.firstNames);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"text-center\">\r\n        <h1 class=\"display-4\">Inventory</h1>\r\n        Name: \"");
#nullable restore
#line 10 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
          Write(Model.InputName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n        <br />\r\n        Price: \"");
#nullable restore
#line 12 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
           Write(Model.InputPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n        <br />\r\n");
#nullable restore
#line 14 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
          
            if (@Model.EX != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h3>**ERROR: ");
#nullable restore
#line 17 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
                        Write(Model.EX.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                <br />\r\n                <hr />\r\n                <br />\r\n                <br />\r\n");
#nullable restore
#line 22 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    First Name\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 34 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
             foreach (var item in Model.firstNames)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 38 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"
                   Write(item);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 41 "C:\Users\wafm2\source\repos\WebApplication3\WebApplication3\Pages\displayInventory.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n    </div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<displayInventoryModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<displayInventoryModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<displayInventoryModel>)PageContext?.ViewData;
        public displayInventoryModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591