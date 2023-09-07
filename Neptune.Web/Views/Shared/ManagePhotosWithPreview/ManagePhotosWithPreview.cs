using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreview : TypedWebPartialViewPage<ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>
    {
        public static void RenderPartialView(IHtmlHelper html, ManagePhotosWithPreviewViewData viewData, ManagePhotosWithPreviewViewModel viewModel)
        {
            html.RenderRazorSitkaPartial<ManagePhotosWithPreview, ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>(viewData, viewModel);
        }
    }
}
