using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;

namespace Neptune.WebMvc.Views.Delineation
{
    public abstract class DelineationMapTemplates : RazorPage
    {

        public static void RenderPartialView(IHtmlHelper html)
        {
            html.RenderRazorSitkaPartial<DelineationMapTemplates>();
        }
    }
}