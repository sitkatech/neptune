using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.Shared.ModeledPerformance
{
    public abstract class ModeledPerformance : TypedWebPartialViewPage<ModeledPerformanceViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ModeledPerformanceViewData viewData)
        {
            html.RenderRazorSitkaPartial<ModeledPerformance, ModeledPerformanceViewData>(viewData);
        }

    }
}