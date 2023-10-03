using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewPercentageSchemaDetail : TypedWebPartialViewPage<ViewPercentageSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewPercentageSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }
    }
}
