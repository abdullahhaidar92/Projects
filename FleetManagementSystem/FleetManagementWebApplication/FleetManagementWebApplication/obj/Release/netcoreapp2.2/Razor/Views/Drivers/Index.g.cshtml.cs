#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7aeae45a0fcf4f0517817cca2999d0ab7830ccb0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Drivers_Index), @"mvc.1.0.view", @"/Views/Drivers/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Drivers/Index.cshtml", typeof(AspNetCore.Views_Drivers_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7aeae45a0fcf4f0517817cca2999d0ab7830ccb0", @"/Views/Drivers/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"169b9f654bcf8c04b3a14822abda27d1205ec27e", @"/Views/_ViewImports.cshtml")]
    public class Views_Drivers_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FleetManagementWebApplication.Models.Driver>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Drivers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Search", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100% ; height:220px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Deliveries", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DriversDeliveries", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
  
    ViewData["Title"] = "Drivers";
    Layout = "~/Views/Shared/LayoutManager.cshtml";

#line default
#line hidden
            BeginContext(161, 10, true);
            WriteLiteral("<br />\r\n\r\n");
            EndContext();
            BeginContext(171, 808, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7aeae45a0fcf4f0517817cca2999d0ab7830ccb06799", async() => {
                BeginContext(222, 422, true);
                WriteLiteral(@"
  
        <table cellspacing=""15px""  align=""right"" style=""background-color: rgb(220,220,220);border:1px grey solid;margin-bottom:20px;"">
            <tr>
                <td class=""td1"" style=""padding-left:20px;color:rgb(50,50,50);font-size:25px;text-align:right;""> Search drivers: </td>
                <td class=""td1"">
                    <input name=""Query"" type=""text""  placeholder=""Search by name or username""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 644, "\"", 670, 1);
#line 14 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 652, ViewData["Query"], 652, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(671, 301, true);
                WriteLiteral(@"
                           style=""height:50px;border:2px solid grey;border-radius:15px;
                                        padding:10px 20px;min-width:450px;
                                        font-size:22px;""/>           
                </td>
            </tr>
        </table>
 
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
            BeginContext(979, 102, true);
            WriteLiteral("\r\n\r\n<div class=\"box-wrapper\">\r\n    <div class=\"box vehicles-container\">\r\n        <div class=\"row\">\r\n\r\n");
            EndContext();
#line 28 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
             foreach (var item in Model)
            {
                int x = (int)item.Score;
                string img = "/images/star" + x + ".png";

#line default
#line hidden
            BeginContext(1239, 131, true);
            WriteLiteral("                <div class=\"col-lg-3 col-md-4\">\r\n                    <div class=\"vehicle-entry effect6 \">\r\n                        ");
            EndContext();
            BeginContext(1370, 69, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7aeae45a0fcf4f0517817cca2999d0ab7830ccb010555", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1380, "~/images/", 1380, 9, true);
#line 34 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
AddHtmlAttributeValue("", 1389, item.Image, 1389, 11, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1439, 30, true);
            WriteLiteral("\r\n                        <h3>");
            EndContext();
            BeginContext(1470, 9, false);
#line 35 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
                       Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1479, 38, true);
            WriteLiteral("   </h3>\r\n                        <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1517, "\"", 1527, 1);
#line 36 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 1523, img, 1523, 4, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1528, 206, true);
            WriteLiteral(" height=\"40\" width=\"90%\" />\r\n                        <table width=\"100%\">\r\n                            <tr>\r\n                                <td>\r\n                                    <h6 class=\"w3-opacity\">");
            EndContext();
            BeginContext(1735, 13, false);
#line 40 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
                                                      Write(item.Username);

#line default
#line hidden
            EndContext();
            BeginContext(1748, 75, true);
            WriteLiteral("</h6>\r\n                                    <h6 class=\"w3-opacity\">Address: ");
            EndContext();
            BeginContext(1824, 12, false);
#line 41 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
                                                               Write(item.Address);

#line default
#line hidden
            EndContext();
            BeginContext(1836, 73, true);
            WriteLiteral("</h6>\r\n                                    <h6 class=\"w3-opacity\">Phone: ");
            EndContext();
            BeginContext(1910, 16, false);
#line 42 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
                                                             Write(item.Phonenumber);

#line default
#line hidden
            EndContext();
            BeginContext(1926, 155, true);
            WriteLiteral(" </h6>\r\n                                </td>\r\n                                <td align=\"right\" style=\"width:100px\">\r\n                                    ");
            EndContext();
            BeginContext(2081, 307, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7aeae45a0fcf4f0517817cca2999d0ab7830ccb015001", async() => {
                BeginContext(2146, 167, true);
                WriteLiteral("\r\n                                        <button class=\"btn-blue\" style=\"width:100%\">Deliveries</button>\r\n                                        <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2313, "\"", 2329, 1);
#line 47 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 2321, item.Id, 2321, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2330, 51, true);
                WriteLiteral(" name=\"Id\" />\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2388, 38, true);
            WriteLiteral("\r\n                                    ");
            EndContext();
            BeginContext(2426, 299, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7aeae45a0fcf4f0517817cca2999d0ab7830ccb017586", async() => {
                BeginContext(2488, 162, true);
                WriteLiteral("\r\n                                        <button class=\"btn-green\" style=\"width:100%\">Edit</button>\r\n                                        <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2650, "\"", 2666, 1);
#line 51 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 2658, item.Id, 2658, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2667, 51, true);
                WriteLiteral(" name=\"Id\" />\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2725, 80, true);
            WriteLiteral("\r\n                                    <button class=\"btn-red\" style=\"width:100%\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2805, "\"", 2866, 7);
            WriteAttributeValue("", 2815, "showDelete(\'", 2815, 12, true);
#line 53 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 2827, item.Name, 2827, 10, false);

#line default
#line hidden
            WriteAttributeValue("", 2837, "\',\'", 2837, 3, true);
#line 53 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 2840, item.Username, 2840, 14, false);

#line default
#line hidden
            WriteAttributeValue("", 2854, "\',", 2854, 2, true);
#line 53 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
WriteAttributeValue("", 2856, item.Id, 2856, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 2864, ");", 2864, 2, true);
            EndWriteAttribute();
            BeginContext(2867, 178, true);
            WriteLiteral(">Delete</button>\r\n                                </td>\r\n                            </tr>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 59 "C:\Users\hp\Desktop\Semester6Projects\Fleet2\FleetManagementWebApplication\FleetManagementWebApplication\Views\Drivers\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(3060, 1136, true);
            WriteLiteral(@"
        </div>
    </div>
</div>


<div class=""modal fade"" id=""DeleteModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLongTitle"" style=""font-size:25px"">Confirm Delete</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"" style=""padding-top:40px;"">
                <h4>Are you sure you want to delete this driver?</h4>

                <h5>Name: <span id=""DriverName"" style=""color: rgba(50,250, 50, 0.9);"">Elio</span> </h5>
                <h5>Username: <span id=""DriverUserName"" style=""color: rgba(50,250, 50, 0.9);""> Elio@gmail.com</span></h5>
            </div>
     ");
            WriteLiteral("       <div class=\"modal-footer\">\r\n                <div style=\"width:80%\" id=\"response\"></div>\r\n                ");
            EndContext();
            BeginContext(4196, 490, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7aeae45a0fcf4f0517817cca2999d0ab7830ccb023320", async() => {
                BeginContext(4261, 418, true);
                WriteLiteral(@"
                    <table>
                        <tr>
                            <td>  <a class=""btn btn-danger"" data-dismiss=""modal"" style=""cursor:pointer"">Cancel</a></td>
                            <td><button type=""submit"" class=""btn btn-primary"">Delete</button></td>
                        </tr>
                    </table>
                    <input type=""hidden"" id=""DriverId"" />
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4686, 1576, true);
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>

<script type=""text/javascript"" src=""http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js""></script>
<script type=""text/javascript"">
    function showDelete(name, username, id) {
        // alert(""Hello"");
        document.getElementById(""DriverName"").innerHTML = name;
        document.getElementById(""DriverUserName"").innerHTML = username;
        document.getElementById(""DriverId"").value = id;
        $(""#DeleteModal"").modal();
    }

</script>

<style>
    .btn1 {
        text-align: right;
    }

    .box {
        text-align: left;
        position: relative;
        padding: 1px 1px;
        width: 95%;
        background: inherit;
        margin: 6px 5px;
    }

    .modal-header {
        background-color: rgba(50,250, 50, 0.7);
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
        color: rgb(10,10, 10);
    }

    .modal-footer {
        background-color: rgba(50,250, ");
            WriteLiteral(@"50, 0.7);
        color: white;
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
        text-align: left;
        float: left;
        clear: both;
    }

    .modal-content {
        background-color: rgba(0,0, 0, 0.5);
        border-radius: 20px;
        color: white;
        height: 350px;
    }

     

            .td1 {
                text-align: center;
                padding: 10px 10px;
                color: white;
                font-size: 18px;
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FleetManagementWebApplication.Models.Driver>> Html { get; private set; }
    }
}
#pragma warning restore 1591
