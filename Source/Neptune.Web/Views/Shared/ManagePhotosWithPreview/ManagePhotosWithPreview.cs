using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreview : TypedWebPartialViewPage<ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>
    {
        public static void RenderPartialView(HtmlHelper html, ManagePhotosWithPreviewViewData viewData, ManagePhotosWithPreviewViewModel viewModel)
        {
            html.RenderRazorSitkaPartial<ManagePhotosWithPreview, ManagePhotosWithPreviewViewData, ManagePhotosWithPreviewViewModel>(viewData, viewModel);
        }
    }
}
