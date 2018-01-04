using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.ObservationType
{
   
    public abstract class ViewRateSchemaDetail : TypedWebPartialViewPage<ViewRateSchemaDetailViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ViewRateSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }
    }
}
