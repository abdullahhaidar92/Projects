#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84868a5847064f066b7ea6ab7cbf16d1d976c2ba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Plans_Details), @"mvc.1.0.view", @"/Views/Plans/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Plans/Details.cshtml", typeof(AspNetCore.Views_Plans_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84868a5847064f066b7ea6ab7cbf16d1d976c2ba", @"/Views/Plans/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"169b9f654bcf8c04b3a14822abda27d1205ec27e", @"/Views/_ViewImports.cshtml")]
    public class Views_Plans_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FleetManagementWebApplication.Models.Plan>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(49, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/LayoutManager.cshtml";

#line default
#line hidden
            BeginContext(142, 84, true);
            WriteLiteral("    <div class=\"box-wrapper\">\r\n        <div class=\"box effect6\">\r\n\r\n            <h2>");
            EndContext();
            BeginContext(227, 10, false);
#line 10 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
           Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(237, 119, true);
            WriteLiteral("</h2>\r\n            <div class=\"w3-container w3-padding\">\r\n\r\n                <h3>Activities</h3>\r\n                <ul>\r\n");
            EndContext();
#line 15 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                     foreach (Activity a in @Model.Activities)
                    {

#line default
#line hidden
            BeginContext(443, 29, true);
            WriteLiteral("                        <li> ");
            EndContext();
            BeginContext(473, 14, false);
#line 17 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                        Write(a.Service.Name);

#line default
#line hidden
            EndContext();
            BeginContext(487, 7, true);
            WriteLiteral(" every ");
            EndContext();
            BeginContext(495, 8, false);
#line 17 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                                              Write(a.Period);

#line default
#line hidden
            EndContext();
            BeginContext(503, 12, true);
            WriteLiteral(" days</li>\r\n");
            EndContext();
#line 18 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"

                    }

#line default
#line hidden
            BeginContext(540, 104, true);
            WriteLiteral("                </ul>\r\n                <hr />\r\n                <h3>Vehicles</h3>\r\n                <ul>\r\n");
            EndContext();
#line 24 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                     foreach (Vehicle v in @Model.Vehicles)
                    {

#line default
#line hidden
            BeginContext(728, 29, true);
            WriteLiteral("                        <li> ");
            EndContext();
            BeginContext(758, 6, false);
#line 26 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                        Write(v.Make);

#line default
#line hidden
            EndContext();
            BeginContext(764, 2, true);
            WriteLiteral("  ");
            EndContext();
            BeginContext(767, 7, false);
#line 26 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                                 Write(v.Model);

#line default
#line hidden
            EndContext();
            BeginContext(774, 2, true);
            WriteLiteral("  ");
            EndContext();
            BeginContext(777, 14, false);
#line 26 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"
                                           Write(v.LicensePlate);

#line default
#line hidden
            EndContext();
            BeginContext(791, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 27 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\Details.cshtml"

                    }

#line default
#line hidden
            BeginContext(823, 1988, true);
            WriteLiteral(@"                </ul>



            </div>
        </div>
        </div>
        <style>

            html, body, h1, h2, h3, h4, h5 {
                font-family: ""Open Sans"", sans-serif
            }

            body {
                background: #ccc
            }

            .box {
                text-align: left;
                position: relative;
                top: 12px;
                padding: 10px 30px;
                width: 60%;
                padding-bottom: 30px;
                background: #FFF;
            }

            .effect6 {
                position: relative;
                -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
                -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
                box-shadow: 0 1px 40px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
            }

                .effect6:before, .effect6:after {
                    content: """";
");
            WriteLiteral(@"                    position: absolute;
                    z-index: -1;
                    -webkit-box-shadow: 0 0 20px rgba(0,0,0,0.8);
                    -moz-box-shadow: 0 0 20px rgba(0,0,0,0.8);
                    box-shadow: 0 0 50px rgba(0,0,0,0.8);
                    top: 50%;
                    bottom: 0;
                    left: 10px;
                    right: 10px;
                    -moz-border-radius: 100px / 10px;
                    border-radius: 100px / 10px;
                }

                .effect6:after {
                    right: 10px;
                    left: auto;
                    -webkit-transform: skew(8deg) rotate(3deg);
                    -moz-transform: skew(8deg) rotate(3deg);
                    -ms-transform: skew(8deg) rotate(3deg);
                    -o-transform: skew(8deg) rotate(3deg);
                    transform: skew(8deg) rotate(3deg);
                }
        </style>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FleetManagementWebApplication.Models.Plan> Html { get; private set; }
    }
}
#pragma warning restore 1591
