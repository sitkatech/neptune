using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewPassFailSchemaDetail : TypedWebPartialViewPage<ViewPassFailSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewPassFailSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }
    }
}
