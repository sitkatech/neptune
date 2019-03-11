using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;

namespace Neptune.Web.Views.Delineation
{
    public abstract class DelineationMapTemplates : WebViewPage
    {

        public static void RenderPartialView(HtmlHelper html)
        {
            html.RenderRazorSitkaPartial<DelineationMapTemplates>();
        }
    }
}