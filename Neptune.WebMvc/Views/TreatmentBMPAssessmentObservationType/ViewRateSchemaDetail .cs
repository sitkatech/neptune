using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewRateSchemaDetail : TypedWebPartialViewPage<ViewRateSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewRateSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }
    }
}
