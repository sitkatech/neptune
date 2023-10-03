using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Shared.ModeledPerformance
{
    public abstract class ModeledPerformance : TypedWebPartialViewPage<ModeledPerformanceViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ModeledPerformanceViewData viewData)
        {
            html.RenderRazorSitkaPartial<ModeledPerformance, ModeledPerformanceViewData>(viewData);
        }

    }
}