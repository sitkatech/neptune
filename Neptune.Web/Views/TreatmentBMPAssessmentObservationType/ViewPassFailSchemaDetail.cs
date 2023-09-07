using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewPassFailSchemaDetail : TypedWebPartialViewPage<ViewPassFailSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewPassFailSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }
    }
}
