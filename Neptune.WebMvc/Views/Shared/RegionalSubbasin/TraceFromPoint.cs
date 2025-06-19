using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Shared.RegionalSubbasin
{
    public abstract class TraceFromPoint : TypedWebPartialViewPage<TraceFromPointViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, TraceFromPointViewData viewData)
        {
            html.RenderRazorSitkaPartial<TraceFromPoint, TraceFromPointViewData>(viewData);
        }
    }
}
