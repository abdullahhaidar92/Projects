#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15013840aff7ff41c815f03a33e34d5ed08b482e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointments_PatientsAppointments), @"mvc.1.0.view", @"/Views/Appointments/PatientsAppointments.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Appointments/PatientsAppointments.cshtml", typeof(AspNetCore.Views_Appointments_PatientsAppointments))]
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
#line 1 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\_ViewImports.cshtml"
using Clinic;

#line default
#line hidden
#line 2 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\_ViewImports.cshtml"
using Clinic.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15013840aff7ff41c815f03a33e34d5ed08b482e", @"/Views/Appointments/PatientsAppointments.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"967d7115af2116ec51148838caed4c3d0a52ac70", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointments_PatientsAppointments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SearchAppointments>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Search", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("srch-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
  
    ViewData["Title"] = "Search";


#line default
#line hidden
            BeginContext(73, 679, true);
            WriteLiteral(@"<style>
    .window {
        width: initial;
    }

    .col-lg-9 {
        padding-left: 10px;
    }

    .col-lg-3 {
        text-align: left;
    }

    .srch-div:hover {
        cursor: initial;
        -webkit-box-shadow: initial;
        -moz-box-shadow: initial;
        box-shadow: initial;
    }

    .srch-div {
        border-bottom: 1px solid silver;
    }
</style>
<div class=""window-wrapper"" style=""padding-top:10px;"">
    <div style=""text-align:left;display:inline-block;min-width:80%;"">
        <div class=""row"">
            <div class=""col-lg-3 col-md-3 "">
                <div class=""window"" style=""width:95%"">
                    ");
            EndContext();
            BeginContext(752, 1013, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "15013840aff7ff41c815f03a33e34d5ed08b482e6168", async() => {
                BeginContext(791, 323, true);
                WriteLiteral(@"
                        <div class=""header"">
                            Search
                        </div>
                        <div class=""body"">
                            <div class=""form-group"">
                                <label class=""control-label"">Doctor</label>
                                ");
                EndContext();
                BeginContext(1114, 82, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "15013840aff7ff41c815f03a33e34d5ed08b482e6880", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#line 43 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Doctor);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 43 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.Doctors;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1196, 200, true);
                WriteLiteral("\r\n                            </div>\r\n                            <div class=\"form-group\">\r\n                                <label class=\"control-label\">Date </label>\r\n                                ");
                EndContext();
                BeginContext(1396, 101, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "15013840aff7ff41c815f03a33e34d5ed08b482e9258", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 47 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.DateTime);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                BeginWriteTagHelperAttribute();
#line 47 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
                                                                          WriteLiteral(Model.DateTime.Date.ToString("yyyy-MM-dd"));

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1497, 261, true);
                WriteLiteral(@"
                            </div>

                        </div>
                        <div class=""footer"">
                            <button type=""submit"" class=""btn btn-success"">Search</button>
                        </div>
                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1765, 472, true);
            WriteLiteral(@"
                </div>

            </div>
            <div class=""col-lg-9 col-md-9 "">
                <div class=""window"" style=""width:100%;"">
                    <div class=""header"">
                        Appointments
                    </div>
                    <div class=""body"" style=""min-height:470px;"">
                        <div style=""font-size:15px;padding-bottom:10px;"">You have appointments with these doctors at the specified times</div>

");
            EndContext();
#line 66 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
                         foreach (var item in Model.Appointments)
                        {


#line default
#line hidden
            BeginContext(2333, 299, true);
            WriteLiteral(@"                            <div class=""srch-div"" style=""width:90%;margin-bottom:3px;"">
                                <table style=""width:100%"">
                                    <tr>
                                        <td style=""width:20%"">
                                            ");
            EndContext();
            BeginContext(2632, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "15013840aff7ff41c815f03a33e34d5ed08b482e14564", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2642, "~/images/", 2642, 9, true);
#line 73 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
AddHtmlAttributeValue("", 2651, item.Doctor.Image, 2651, 18, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2690, 321, true);
            WriteLiteral(@"
                                        </td>
                                        <td>
                                            <div style=""padding-left:20px;padding-bottom:5px;padding-top:5px;font-size:15px;"">

                                                <div style=""font-size:20px;color:rgb(0,0,250);"">");
            EndContext();
            BeginContext(3012, 53, false);
#line 78 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
                                                                                           Write(Html.DisplayFor(modelItem => item.Doctor.DisplayName));

#line default
#line hidden
            EndContext();
            BeginContext(3065, 182, true);
            WriteLiteral("</div>\r\n\r\n                                                \r\n                                                <br />\r\n\r\n                                                Date and Time:  ");
            EndContext();
            BeginContext(3248, 43, false);
#line 83 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"
                                                           Write(Html.DisplayFor(modelItem => item.DateTime));

#line default
#line hidden
            EndContext();
            BeginContext(3291, 330, true);
            WriteLiteral(@"
                                                <br />

                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
");
            EndContext();
#line 92 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Appointments\PatientsAppointments.cshtml"


                        }

#line default
#line hidden
            BeginContext(3652, 172, true);
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SearchAppointments> Html { get; private set; }
    }
}
#pragma warning restore 1591
