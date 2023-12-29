using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewDiscreteValueSchemaDetail : TypedWebPartialViewPage<ViewDiscreteValueSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewDiscreteValueSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }
    }
}
