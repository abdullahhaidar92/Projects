#pragma checksum "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1927325c3466a2c2b3bb870d0adff75b97d4857c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consultations_Treatment), @"mvc.1.0.view", @"/Views/Consultations/Treatment.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Consultations/Treatment.cshtml", typeof(AspNetCore.Views_Consultations_Treatment))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1927325c3466a2c2b3bb870d0adff75b97d4857c", @"/Views/Consultations/Treatment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"967d7115af2116ec51148838caed4c3d0a52ac70", @"/Views/_ViewImports.cshtml")]
    public class Views_Consultations_Treatment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 191, true);
            WriteLiteral("<div class=\"window-wrapper\">\r\n\r\n    <div class=\"window\">\r\n        <div class=\"header\" style=\"text-align:center\">\r\n            <h1>Treatments</h1>\r\n        </div>\r\n        <div class=\"body\">\r\n");
            EndContext();
#line 8 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
             foreach (var doctor in ViewBag.doctors)
            {

#line default
#line hidden
            BeginContext(260, 74, true);
            WriteLiteral("                <div class=\"srch-div\" style=\"width:90%;margin-bottom:3px;\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 334, "\"", 373, 3);
            WriteAttributeValue("", 344, "OpenTreatment(", 344, 14, true);
#line 10 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
WriteAttributeValue("", 358, doctor.Key.Id, 358, 14, false);

#line default
#line hidden
            WriteAttributeValue("", 372, ")", 372, 1, true);
            EndWriteAttribute();
            BeginContext(374, 30, true);
            WriteLiteral(">\r\n                    <h2> + ");
            EndContext();
            BeginContext(405, 22, false);
#line 11 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                      Write(doctor.Key.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(427, 31, true);
            WriteLiteral("</h2>\r\n                </div>\r\n");
            EndContext();
            BeginContext(462, 43, true);
            WriteLiteral("                    <div class=\"modal fade\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 505, "\"", 524, 1);
#line 15 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
WriteAttributeValue("", 510, doctor.Key.Id, 510, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(525, 509, true);
            WriteLiteral(@" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
                        <div class=""modal-dialog modal-dialog-centered modal-lg"" role=""document"">
                            <div class=""modal-content""  style=""background-color:rgba(250,250,250,0.8)"">
                                <div class=""modal-header"" style=""background-color:rgba(250,0,0,0.9)"">
                                    <h5 class=""modal-title"" id=""exampleModalLongTitle"" style=""font-size:25px"">");
            EndContext();
            BeginContext(1035, 22, false);
#line 19 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                         Write(doctor.Key.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(1057, 446, true);
            WriteLiteral(@" Treatments</h5>
                                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                                        <span aria-hidden=""true"">&times;</span>
                                    </button>
                                </div>
                                <div class=""modal-body"" style=""padding-bottom:40px;min-height:300px;background-color:rgba(100,100,250,0.5)"">


");
            EndContext();
#line 27 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                     foreach (Consultation c in doctor.Value)
                                    {

#line default
#line hidden
            BeginContext(1621, 98, true);
            WriteLiteral("                                        <div class=\"srch-div\" style=\"width:90%;margin-bottom:3px;\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1719, "\"", 1754, 3);
            WriteAttributeValue("", 1729, "OpenConsultation(\'", 1729, 18, true);
#line 29 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
WriteAttributeValue("", 1747, c.Id, 1747, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 1752, "\')", 1752, 2, true);
            EndWriteAttribute();
            BeginContext(1755, 99, true);
            WriteLiteral(">\r\n                                            <div style=\"font-size:36px;font-family:Calibri\"> +  ");
            EndContext();
            BeginContext(1855, 7, false);
#line 30 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                           Write(c.Title);

#line default
#line hidden
            EndContext();
            BeginContext(1862, 28, true);
            WriteLiteral("  <span style=\"float:right\">");
            EndContext();
            BeginContext(1891, 29, false);
#line 30 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                                               Write(c.Date.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1920, 63, true);
            WriteLiteral("</span></div>\r\n                                        </div>\r\n");
            EndContext();
            BeginContext(1985, 63, true);
            WriteLiteral("                                        <div class=\"modal fade\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2048, "\"", 2064, 1);
#line 33 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
WriteAttributeValue("", 2053, "C"+c.Id, 2053, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2065, 656, true);
            WriteLiteral(@" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
                                            <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                                                <div class=""modal-content"" style=""border-radius:10px;background-color: rgba(0, 0,0, 0.3)"">
                                                    <div class=""modal-header"" style=""background-color: rgba(0,200, 0, 0.9);border-top-left-radius:10px;border-top-right-radius:10px;"">
                                                        <h5 class=""modal-title"" id=""exampleModalLongTitle"" style=""font-size:25px"">");
            EndContext();
            BeginContext(2722, 7, false);
#line 37 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                                             Write(c.Title);

#line default
#line hidden
            EndContext();
            BeginContext(2729, 98, true);
            WriteLiteral("</h5>\r\n                                                        <button type=\"button\" class=\"close\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2827, "\"", 2851, 3);
            WriteAttributeValue("", 2837, "Close(\'", 2837, 7, true);
#line 38 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
WriteAttributeValue("", 2844, c.Id, 2844, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 2849, "\')", 2849, 2, true);
            EndWriteAttribute();
            BeginContext(2852, 463, true);
            WriteLiteral(@" aria-label=""Close"">
                                                            <span aria-hidden=""true"">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class=""modal-body"" style=""background-color:rgb(250,250,250)"">
                                                        <h4><span class=""green"">Type: </span>");
            EndContext();
            BeginContext(3316, 6, false);
#line 43 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                        Write(c.Type);

#line default
#line hidden
            EndContext();
            BeginContext(3322, 107, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Temperature:</span> ");
            EndContext();
            BeginContext(3430, 13, false);
#line 44 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                               Write(c.Temperature);

#line default
#line hidden
            EndContext();
            BeginContext(3443, 110, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Blood Pressure:</span> ");
            EndContext();
            BeginContext(3554, 15, false);
#line 45 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                  Write(c.BloodPressure);

#line default
#line hidden
            EndContext();
            BeginContext(3569, 100, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Cost:</span> ");
            EndContext();
            BeginContext(3670, 6, false);
#line 46 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                        Write(c.Cost);

#line default
#line hidden
            EndContext();
            BeginContext(3676, 110, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Symptoms:</span><br /> ");
            EndContext();
            BeginContext(3787, 10, false);
#line 47 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                  Write(c.Symptoms);

#line default
#line hidden
            EndContext();
            BeginContext(3797, 112, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Diagnosis: </span><br /> ");
            EndContext();
            BeginContext(3910, 11, false);
#line 48 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                    Write(c.Diagnosis);

#line default
#line hidden
            EndContext();
            BeginContext(3921, 112, true);
            WriteLiteral("</h4>\r\n                                                        <h4><span class=\"green\">Treatment:</span> <br /> ");
            EndContext();
            BeginContext(4034, 11, false);
#line 49 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                                                                                                    Write(c.Treatment);

#line default
#line hidden
            EndContext();
            BeginContext(4045, 663, true);
            WriteLiteral(@"</h4>
                                                    </div>
                                                    <div class=""modal-footer "" style=""background-color: rgba(0,200, 0, 0.9);border-bottom-left-radius:10px;border-bottom-right-radius:10px;"">
                                                        <div style=""text-align:center;display:inline-block;width:100%"">

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
");
            EndContext();
#line 60 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"







                                    }

#line default
#line hidden
            BeginContext(4761, 268, true);
            WriteLiteral(@"


                                </div>
                                    </div>

                                </div>
                                <div class=""modal-footer"">
                                </div>
                            </div>
");
            EndContext();
#line 78 "C:\Users\hp\Desktop\Semester6Projects\Clinic\Clinic\Views\Consultations\Treatment.cshtml"
                      
            }

#line default
#line hidden
            BeginContext(5068, 325, true);
            WriteLiteral(@"
            </div>

            </div>
</div>


<script type=""text/javascript"">
    function OpenTreatment(id) {
        $(""#"" + id).modal();
    }
    function OpenConsultation(id) {
        $(""#C"" + id).modal();
        }
        function Close(id) {
        $(""#C""+id).modal('hide');
    }
   
</script>");
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
