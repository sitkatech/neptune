using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewPercentageSchemaDetail : TypedWebPartialViewPage<ViewPercentageSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewPercentageSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }
    }
}
