#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consultations_Index), @"mvc.1.0.view", @"/Views/Consultations/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Consultations/Index.cshtml", typeof(AspNetCore.Views_Consultations_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb", @"/Views/Consultations/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"967d7115af2116ec51148838caed4c3d0a52ac70", @"/Views/_ViewImports.cshtml")]
    public class Views_Consultations_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Clinic.Models.Consultation>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(48, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
  
    ViewData["Title"] = "Index";
      string Role = (string)ViewData["Role"];

#line default
#line hidden
            BeginContext(138, 325, true);
            WriteLiteral(@"<style>
    td {
        padding: 10px 8px;
        width: 15%;
    }

    th {
        padding: 10px 8px;
        width: 15%;
    }
</style>

<div class=""table1"" style="" background-color: rgba(0, 148, 255, 0.5);"">
    <table width=""100%"" class=""all-table-th"">

        <tr>
            <th>
                ");
            EndContext();
            BeginContext(464, 4, false);
#line 24 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
           Write(Html);

#line default
#line hidden
            EndContext();
            BeginContext(468, 92, true);
            WriteLiteral("..DisplayNameFor(model => model.Date)\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(561, 4, false);
#line 27 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
           Write(Html);

#line default
#line hidden
            EndContext();
            BeginContext(565, 59, true);
            WriteLiteral("..DisplayNameFor(model => model.Title)\r\n            </th>\r\n");
            EndContext();
#line 29 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
             if (Role == "Admin" || Role == "Patient")
            {

#line default
#line hidden
            BeginContext(695, 73, true);
            WriteLiteral("                <th>\r\n                    Doctor\r\n                </th>\r\n");
            EndContext();
#line 34 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(783, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 35 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
             if (Role == "Admin" || Role == "Doctor")
            {

#line default
#line hidden
            BeginContext(853, 74, true);
            WriteLiteral("                <th>\r\n                    Patient\r\n                </th>\r\n");
            EndContext();
#line 40 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(942, 54, true);
            WriteLiteral("\r\n            <th style=\"width:10%\">\r\n                ");
            EndContext();
            BeginContext(997, 4, false);
#line 43 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
           Write(Html);

#line default
#line hidden
            EndContext();
            BeginContext(1001, 118, true);
            WriteLiteral("..DisplayNameFor(model => model.Temperature)\r\n            </th>\r\n            <th style=\"width:13%\" >\r\n                ");
            EndContext();
            BeginContext(1120, 4, false);
#line 46 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
           Write(Html);

#line default
#line hidden
            EndContext();
            BeginContext(1124, 103, true);
            WriteLiteral("..DisplayNameFor(model => model.BloodPressure)\r\n            </th>\r\n            <th  >\r\n                ");
            EndContext();
            BeginContext(1228, 4, false);
#line 49 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
           Write(Html);

#line default
#line hidden
            EndContext();
            BeginContext(1232, 140, true);
            WriteLiteral("..DisplayNameFor(model => model.Cost)\r\n            </th>\r\n            <th style=\"width:25%\"></th>\r\n        </tr>\r\n    </table>\r\n    <br />\r\n");
            EndContext();
#line 55 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(1411, 88, true);
            WriteLiteral("<table class=\"all-table\">\r\n         <tr>\r\n             <td class=\"time\">\r\n              ");
            EndContext();
            BeginContext(1500, 29, false);
#line 59 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
         Write(item.Date.ToShortDateString());

#line default
#line hidden
            EndContext();
            BeginContext(1529, 60, true);
            WriteLiteral("\r\n\r\n             </td>\r\n             <td>\r\n                 ");
            EndContext();
            BeginContext(1590, 40, false);
#line 63 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            Write(Html.DisplayFor(modelItem => item.Title));

#line default
#line hidden
            EndContext();
            BeginContext(1630, 24, true);
            WriteLiteral("\r\n             </td>\r\n\r\n");
            EndContext();
#line 66 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
              if (Role == "Admin" || Role == "Patient")
             {

#line default
#line hidden
            BeginContext(1727, 44, true);
            WriteLiteral("                 <td>\r\n                     ");
            EndContext();
            BeginContext(1772, 23, false);
#line 69 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
                Write(item.Doctor.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(1795, 26, true);
            WriteLiteral("\r\n                 </td>\r\n");
            EndContext();
#line 71 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
             }

#line default
#line hidden
            BeginContext(1837, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 73 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
              if (Role == "Admin" || Role == "Doctor")
             {

#line default
#line hidden
            BeginContext(1911, 44, true);
            WriteLiteral("                 <td>\r\n                     ");
            EndContext();
            BeginContext(1956, 24, false);
#line 76 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
                Write(item.Patient.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(1980, 26, true);
            WriteLiteral("\r\n                 </td>\r\n");
            EndContext();
#line 78 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
             }

#line default
#line hidden
            BeginContext(2022, 54, true);
            WriteLiteral("             <td style=\"width:10%\">\r\n                 ");
            EndContext();
            BeginContext(2077, 46, false);
#line 80 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            Write(Html.DisplayFor(modelItem => item.Temperature));

#line default
#line hidden
            EndContext();
            BeginContext(2123, 76, true);
            WriteLiteral("\r\n             </td>\r\n             <td style=\"width:13%\">\r\n                 ");
            EndContext();
            BeginContext(2200, 48, false);
#line 83 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            Write(Html.DisplayFor(modelItem => item.BloodPressure));

#line default
#line hidden
            EndContext();
            BeginContext(2248, 60, true);
            WriteLiteral("\r\n             </td>\r\n             <td  >\r\n                 ");
            EndContext();
            BeginContext(2309, 39, false);
#line 86 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
            Write(Html.DisplayFor(modelItem => item.Cost));

#line default
#line hidden
            EndContext();
            BeginContext(2348, 78, true);
            WriteLiteral("\r\n             </td>\r\n\r\n             <td style=\"width:25%\">\r\n                 ");
            EndContext();
            BeginContext(2426, 486, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb14795", async() => {
                BeginContext(2445, 48, true);
                WriteLiteral("\r\n                   \r\n                         ");
                EndContext();
                BeginContext(2493, 102, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb15229", async() => {
                    BeginContext(2582, 4, true);
                    WriteLiteral("Edit");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 92 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
                                                                                            WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2595, 25, true);
                WriteLiteral("\r\n\r\n                     ");
                EndContext();
                BeginContext(2620, 108, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb17972", async() => {
                    BeginContext(2712, 7, true);
                    WriteLiteral("Details");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 94 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
                                                                                           WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2728, 49, true);
                WriteLiteral("\r\n                    \r\n                         ");
                EndContext();
                BeginContext(2777, 105, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ed855d72851a323e4ad4bc8bc8ac7d93fd3b6eb20741", async() => {
                    BeginContext(2867, 6, true);
                    WriteLiteral("Delete");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 96 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
                                                                                             WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2882, 23, true);
                WriteLiteral("\r\n\r\n\r\n                 ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2912, 58, true);
            WriteLiteral("\r\n\r\n             </td>\r\n         </tr>\r\n        </table>\r\n");
            EndContext();
#line 104 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Index.cshtml"
    }

#line default
#line hidden
            BeginContext(2977, 10, true);
            WriteLiteral("\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Clinic.Models.Consultation>> Html { get; private set; }
    }
}
#pragma warning restore 1591