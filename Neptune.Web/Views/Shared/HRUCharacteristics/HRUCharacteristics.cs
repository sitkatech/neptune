using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public abstract class HRUCharacteristics : TypedWebPartialViewPage<HRUCharacteristicsViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, HRUCharacteristicsViewData viewData)
        {
            html.RenderRazorSitkaPartial<HRUCharacteristics, HRUCharacteristicsViewData>(viewData);
        }
    }
}