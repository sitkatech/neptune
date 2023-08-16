using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.Home
{
    public abstract class LaunchPad : TypedWebPartialViewPage<LaunchPadViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, LaunchPadViewData viewData)
        {
            html.RenderRazorSitkaPartial<LaunchPad, LaunchPadViewData>(viewData);
        }
    }
}
