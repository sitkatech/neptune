using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;

namespace Neptune.Web.Views.Delineation
{
    public abstract class DelineationMapTemplates : RazorPage
    {

        public static void RenderPartialView(IHtmlHelper html)
        {
            html.RenderRazorSitkaPartial<DelineationMapTemplates>();
        }
    }
}