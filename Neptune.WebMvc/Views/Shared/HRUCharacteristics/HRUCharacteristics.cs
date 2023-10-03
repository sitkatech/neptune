using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Shared.HRUCharacteristics
{
    public abstract class HRUCharacteristics : TypedWebPartialViewPage<HRUCharacteristicsViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, HRUCharacteristicsViewData viewData)
        {
            html.RenderRazorSitkaPartial<HRUCharacteristics, HRUCharacteristicsViewData>(viewData);
        }
    }
}