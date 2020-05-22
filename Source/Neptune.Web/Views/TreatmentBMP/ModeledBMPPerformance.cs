using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMP
{
    public abstract class ModeledBMPPerformance : TypedWebPartialViewPage<ModeledBMPPerformanceViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ModeledBMPPerformanceViewData viewData)
        {
            html.RenderRazorSitkaPartial<ModeledBMPPerformance, ModeledBMPPerformanceViewData>(viewData);
        }

    }
}