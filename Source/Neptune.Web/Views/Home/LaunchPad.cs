using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.Home
{
    public abstract class LaunchPad : TypedWebPartialViewPage<LaunchPadViewData>
    {
        public static void RenderPartialView(HtmlHelper html, LaunchPadViewData viewData)
        {
            html.RenderRazorSitkaPartial<LaunchPad, LaunchPadViewData>(viewData);
        }
    }
}
