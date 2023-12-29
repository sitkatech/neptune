using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Shared
{
    public abstract class ViewPageContent : TypedWebPartialViewPage<ViewPageContentViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, ViewPageContentViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPageContent, ViewPageContentViewData>(viewData);
        }
    }
}
