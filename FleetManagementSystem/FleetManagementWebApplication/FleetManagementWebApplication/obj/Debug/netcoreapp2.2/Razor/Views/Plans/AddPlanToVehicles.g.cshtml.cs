#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7afb4559c2a3b38082598da827ada663d8c98afd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Plans_AddPlanToVehicles), @"mvc.1.0.view", @"/Views/Plans/AddPlanToVehicles.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Plans/AddPlanToVehicles.cshtml", typeof(AspNetCore.Views_Plans_AddPlanToVehicles))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7afb4559c2a3b38082598da827ada663d8c98afd", @"/Views/Plans/AddPlanToVehicles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"169b9f654bcf8c04b3a14822abda27d1205ec27e", @"/Views/_ViewImports.cshtml")]
    public class Views_Plans_AddPlanToVehicles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Plans", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SearchAddPlanToVehicles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "FinishAddPlanToVehicles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
  
    ViewData["Title"] = "Add Vehicles";
    Layout = "LayoutManager";
    Vehicle[] Vehicles = (Vehicle[])ViewData["Vehicles"];

#line default
#line hidden
            BeginContext(138, 252, true);
            WriteLiteral("    <div class=\"box-wrapper\">\r\n        <div class=\"box effect6\">\r\n\r\n            <table width=\"100%\">\r\n                <tr>\r\n                    <td><h3>Assign Plan to Vehicles </h3></td>\r\n                    <td align=\"right\">\r\n                        ");
            EndContext();
            BeginContext(390, 309, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7afb4559c2a3b38082598da827ada663d8c98afd5132", async() => {
                BeginContext(456, 129, true);
                WriteLiteral("\r\n                            <input type=\"text\" placeholder=\"filter vehicles\" onkeyup=\"search(event);\" style=\"padding-left:5px;\"");
                EndContext();
                BeginWriteAttribute("value", "\r\n                                   value=\"", 585, "\"", 648, 1);
#line 15 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
WriteAttributeValue("", 629, ViewData["Query1"], 629, 19, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(649, 43, true);
                WriteLiteral(" name=\"Query1\" />\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(699, 143, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n\r\n            <div class=\"w3-container w3-padding\">\r\n                ");
            EndContext();
            BeginContext(842, 3908, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7afb4559c2a3b38082598da827ada663d8c98afd7841", async() => {
                BeginContext(908, 31, true);
                WriteLiteral("\r\n                    <table>\r\n");
                EndContext();
#line 24 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                         for (int i = 0; i < Vehicles.Length; i += 4)
                        {

#line default
#line hidden
                BeginContext(1037, 326, true);
                WriteLiteral(@"                            <tr>
                                <td class=""td1"">
                                    <div style="" background-color: #E8E8E8;padding-bottom:5px;padding-top:5px;padding-left:5px"">
                                        <label class=""container1"">
                                            ");
                EndContext();
                BeginContext(1364, 16, false);
#line 30 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                       Write(Vehicles[i].Make);

#line default
#line hidden
                EndContext();
                BeginContext(1380, 3, true);
                WriteLiteral("   ");
                EndContext();
                BeginContext(1384, 17, false);
#line 30 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                                           Write(Vehicles[i].Model);

#line default
#line hidden
                EndContext();
                BeginContext(1401, 58, true);
                WriteLiteral("<br />\r\n                                            &nbsp;");
                EndContext();
                BeginContext(1460, 24, false);
#line 31 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                             Write(Vehicles[i].LicensePlate);

#line default
#line hidden
                EndContext();
                BeginContext(1484, 92, true);
                WriteLiteral("\r\n                                            <input type=\"checkbox\" name=\"SelectedVehicles\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1576, "\"", 1599, 1);
#line 32 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
WriteAttributeValue("", 1584, Vehicles[i].Id, 1584, 15, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1600, 217, true);
                WriteLiteral(">\r\n                                            <span class=\"checkmark\"></span>\r\n                                        </label>\r\n\r\n                                    </div>\r\n                                </td>\r\n\r\n");
                EndContext();
#line 39 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                 if (i + 1 < Vehicles.Length)
                                {

#line default
#line hidden
                BeginContext(1915, 276, true);
                WriteLiteral(@"                            <td class=""td1"">
                                <div style="" background-color: #E8E8E8;padding-bottom:5px;padding-top:5px;padding-left:5px"">
                                    <label class=""container1"">
                                        ");
                EndContext();
                BeginContext(2192, 20, false);
#line 44 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                   Write(Vehicles[i + 1].Make);

#line default
#line hidden
                EndContext();
                BeginContext(2212, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(2214, 21, false);
#line 44 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                                         Write(Vehicles[i + 1].Model);

#line default
#line hidden
                EndContext();
                BeginContext(2235, 54, true);
                WriteLiteral("<br />\r\n                                        &nbsp;");
                EndContext();
                BeginContext(2290, 28, false);
#line 45 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                         Write(Vehicles[i + 1].LicensePlate);

#line default
#line hidden
                EndContext();
                BeginContext(2318, 88, true);
                WriteLiteral("\r\n                                        <input type=\"checkbox\" name=\"SelectedVehicles\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2406, "\"", 2431, 1);
#line 46 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
WriteAttributeValue("", 2414, Vehicles[i+1].Id, 2414, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2432, 197, true);
                WriteLiteral(">\r\n                                        <span class=\"checkmark\"></span>\r\n                                    </label>\r\n                                </div>\r\n                            </td>\r\n");
                EndContext();
#line 51 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                }

#line default
#line hidden
                BeginContext(2664, 32, true);
                WriteLiteral("                                ");
                EndContext();
#line 52 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                 if (i + 2 < Vehicles.Length)
                                {

#line default
#line hidden
                BeginContext(2762, 276, true);
                WriteLiteral(@"                            <td class=""td1"">
                                <div style="" background-color: #E8E8E8;padding-bottom:5px;padding-top:5px;padding-left:5px"">
                                    <label class=""container1"">
                                        ");
                EndContext();
                BeginContext(3039, 20, false);
#line 57 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                   Write(Vehicles[i + 2].Make);

#line default
#line hidden
                EndContext();
                BeginContext(3059, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(3061, 21, false);
#line 57 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                                         Write(Vehicles[i + 2].Model);

#line default
#line hidden
                EndContext();
                BeginContext(3082, 55, true);
                WriteLiteral("<br />\r\n                                        &nbsp; ");
                EndContext();
                BeginContext(3138, 28, false);
#line 58 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                          Write(Vehicles[i + 2].LicensePlate);

#line default
#line hidden
                EndContext();
                BeginContext(3166, 88, true);
                WriteLiteral("\r\n                                        <input type=\"checkbox\" name=\"SelectedVehicles\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3254, "\"", 3279, 1);
#line 59 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
WriteAttributeValue("", 3262, Vehicles[i+2].Id, 3262, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3280, 197, true);
                WriteLiteral(">\r\n                                        <span class=\"checkmark\"></span>\r\n                                    </label>\r\n                                </div>\r\n                            </td>\r\n");
                EndContext();
#line 64 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                }

#line default
#line hidden
                BeginContext(3512, 32, true);
                WriteLiteral("                                ");
                EndContext();
#line 65 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                 if (i + 3 < Vehicles.Length)
                                {

#line default
#line hidden
                BeginContext(3610, 276, true);
                WriteLiteral(@"                            <td class=""td1"">
                                <div style="" background-color: #E8E8E8;padding-bottom:5px;padding-top:5px;padding-left:5px"">
                                    <label class=""container1"">
                                        ");
                EndContext();
                BeginContext(3887, 20, false);
#line 70 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                   Write(Vehicles[i + 3].Make);

#line default
#line hidden
                EndContext();
                BeginContext(3907, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(3909, 21, false);
#line 70 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                                         Write(Vehicles[i + 3].Model);

#line default
#line hidden
                EndContext();
                BeginContext(3930, 55, true);
                WriteLiteral("<br />\r\n                                        &nbsp; ");
                EndContext();
                BeginContext(3986, 28, false);
#line 71 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                          Write(Vehicles[i + 3].LicensePlate);

#line default
#line hidden
                EndContext();
                BeginContext(4014, 88, true);
                WriteLiteral("\r\n                                        <input type=\"checkbox\" name=\"SelectedVehicles\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 4102, "\"", 4127, 1);
#line 72 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
WriteAttributeValue("", 4110, Vehicles[i+3].Id, 4110, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(4128, 197, true);
                WriteLiteral(">\r\n                                        <span class=\"checkmark\"></span>\r\n                                    </label>\r\n                                </div>\r\n                            </td>\r\n");
                EndContext();
#line 77 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                                }

#line default
#line hidden
                BeginContext(4360, 35, true);
                WriteLiteral("                            </tr>\r\n");
                EndContext();
#line 79 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
                        }

#line default
#line hidden
                BeginContext(4422, 321, true);
                WriteLiteral(@"                    </table>
                    <br>
                    <br>



                    <button type=""submit"" class=""w3-button w3-theme w3-right"">
                        Next
                        <!-- <i class='fas fa-angle-double-right'></i> -->
                    </button>
                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4750, 48, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(4824, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 96 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Plans\AddPlanToVehicles.cshtml"
              await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
                BeginContext(4902, 8, true);
                WriteLiteral("        ");
                EndContext();
            }
            );
            BeginContext(4913, 4860, true);
            WriteLiteral(@"
        <script>
            function search(event) {
                // Number 13 is the ""Enter"" key on the keyboard
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    document.getElementById(""myForm"").submit();
                }
            }
        </script>
        <style>

            html, body, h1, h2, h3, h4, h5 {
                font-family: ""Open Sans"", sans-serif
            }

            .td1 {
                width: 180px;
                height: 50px;
               
                padding: 10px 10px
            }

            body {
                background: #ccc
            }

            .box {
                text-align: left;
                position: relative;
                top: 30px;
                padding: 10px 30px;
            }

            .box {
               ");
            WriteLiteral(@" width: 60%;
                padding-bottom: 40px;
                background: #FFF;
                margin: 40px auto;
            }
            /* Customize the label (the container) */
            .container1 {
                display: block;
                position: relative;
                padding-left: 35px;
                margin-bottom: 12px;
                cursor: pointer;
                font-size: 18px;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

                /* Hide the browser's default checkbox */
                .container1 input {
                    position: absolute;
                    opacity: 0;
                    cursor: pointer;
                    height: 0;
                    width: 0;
                }

            /* Create a custom checkbox */
            .checkmark {
                position: absolute;
             ");
            WriteLiteral(@"   top: 0;
                left: 0;
                height: 25px;
                width: 25px;
                   background-color: rgba(9, 226, 233, 0.30);
            }

            /* On mouse-over, add a grey background color */
            .container1:hover input ~ .checkmark {
                background-color: rgba(9, 226, 233, 0.30);
            }

            /* When the checkbox is checked, add a blue background */
            .container1 input:checked ~ .checkmark {
                background-color: #2196F3;
            }

            /* Create the checkmark/indicator (hidden when not checked) */
            .checkmark:after {
                content: """";
                position: absolute;
                display: none;
            }

            /* Show the checkmark when checked */
            .container1 input:checked ~ .checkmark:after {
                display: block;
            }

            /* Style the checkmark/indicator */
            .container1 .checkmark");
            WriteLiteral(@":after {
                left: 9px;
                top: 5px;
                width: 5px;
                height: 10px;
                border: solid white;
                border-width: 0 3px 3px 0;
                -webkit-transform: rotate(45deg);
                -ms-transform: rotate(45deg);
                transform: rotate(45deg);
            }

            .effect6 {
                position: relative;
                -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
                -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
                box-shadow: 0 1px 40px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;
            }

                .effect6:before, .effect6:after {
                    content: """";
                    position: absolute;
                    z-index: -1;
                    -webkit-box-shadow: 0 0 20px rgba(0,0,0,0.8);
                    -moz-box-shadow: 0 0 20px rgba(0,0,0,0");
            WriteLiteral(@".8);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
