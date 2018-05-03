using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewPercentageSchemaDetail : TypedWebPartialViewPage<ViewPercentageSchemaDetailViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ViewPercentageSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }
    }
}
