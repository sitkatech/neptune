using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreview : TypedWebPartialViewPage<ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>
    {
        public static void RenderPartialView(IHtmlHelper html, ManagePhotosWithPreviewViewData viewData, ManagePhotosWithPreviewViewModel viewModel)
        {
            html.RenderRazorSitkaPartial<ManagePhotosWithPreview, ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>(viewData, viewModel);
        }
    }
}
