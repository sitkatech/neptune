using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewRateSchemaDetail : TypedWebPartialViewPage<ViewRateSchemaDetailViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewRateSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }
    }
}
