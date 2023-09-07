using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewDiscreteValueSchemaDetail : TypedWebPartialViewPage<ViewDiscreteValueSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewDiscreteValueSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }
    }
}
