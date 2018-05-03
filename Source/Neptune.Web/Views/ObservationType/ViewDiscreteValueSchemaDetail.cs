using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
   
    public abstract class ViewDiscreteValueSchemaDetail : TypedWebPartialViewPage<ViewDiscreteValueSchemaDetailViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ViewDiscreteValueSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }
    }
}
