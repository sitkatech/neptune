using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.ObservationType
{
   
    public abstract class ViewPassFailSchemaDetail : TypedWebPartialViewPage<ViewPassFailSchemaDetailViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ViewPassFailSchemaDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }
    }
}
