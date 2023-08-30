using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.Shared.ModeledPerformance
{
    public abstract class ModeledPerformance : TypedWebPartialViewPage<ModeledPerformanceViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ModeledPerformanceViewData viewData)
        {
            html.RenderRazorSitkaPartial<ModeledPerformance, ModeledPerformanceViewData>(viewData);
        }

    }
}