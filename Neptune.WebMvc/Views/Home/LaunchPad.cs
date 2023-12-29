using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Home
{
    public abstract class LaunchPad : TypedWebPartialViewPage<LaunchPadViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, LaunchPadViewData viewData)
        {
            html.RenderRazorSitkaPartial<LaunchPad, LaunchPadViewData>(viewData);
        }
    }
}
