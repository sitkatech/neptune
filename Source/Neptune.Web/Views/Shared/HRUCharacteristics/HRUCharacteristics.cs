using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;
using System.Web.Mvc;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public abstract class HRUCharacteristics : TypedWebPartialViewPage<HRUCharacteristicsViewData>
    {
        public static void RenderPartialView(HtmlHelper html, HRUCharacteristicsViewData viewData)
        {
            html.RenderRazorSitkaPartial<HRUCharacteristics, HRUCharacteristicsViewData>(viewData);
        }
    }
}