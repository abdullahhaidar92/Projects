#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "491d91b0a9c30f83076278c3d4107c7ac476afe9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Map_ShowVehicles), @"mvc.1.0.view", @"/Views/Map/ShowVehicles.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Map/ShowVehicles.cshtml", typeof(AspNetCore.Views_Map_ShowVehicles))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\_ViewImports.cshtml"
using FleetManagementWebApplication;

#line default
#line hidden
#line 2 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\_ViewImports.cshtml"
using FleetManagementWebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"491d91b0a9c30f83076278c3d4107c7ac476afe9", @"/Views/Map/ShowVehicles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"169b9f654bcf8c04b3a14822abda27d1205ec27e", @"/Views/_ViewImports.cshtml")]
    public class Views_Map_ShowVehicles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
  
    ViewData["Title"] = "ShowVehicles";

#line default
#line hidden
            BeginContext(50, 27, true);
            WriteLiteral("\r\n<h1>ShowVehicles</h1>\r\n\r\n");
            EndContext();
#line 8 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
   
    foreach(var v in (List<Vehicle>)ViewData["Vehicles"])
    {

#line default
#line hidden
            BeginContext(148, 39, true);
            WriteLiteral("        <div>\r\n            Vehicle ID: ");
            EndContext();
            BeginContext(188, 4, false);
#line 12 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
                   Write(v.Id);

#line default
#line hidden
            EndContext();
            BeginContext(192, 60, true);
            WriteLiteral(" <br />\r\n            Vehicle Deliveries:\r\n            <ul>\r\n");
            EndContext();
#line 15 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
                 foreach(var d in v.Deliveries)
                {

#line default
#line hidden
            BeginContext(320, 63, true);
            WriteLiteral("                    <li>\r\n                        Delivery ID: ");
            EndContext();
            BeginContext(384, 4, false);
#line 18 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
                                Write(d.Id);

#line default
#line hidden
            EndContext();
            BeginContext(388, 29, true);
            WriteLiteral("\r\n                    </li>\r\n");
            EndContext();
#line 20 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
                }

#line default
#line hidden
            BeginContext(436, 35, true);
            WriteLiteral("            </ul>\r\n        </div>\r\n");
            EndContext();
#line 23 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Map\ShowVehicles.cshtml"
    }

#line default
#line hidden
            BeginContext(481, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591