using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Security;
using Neptune.Web.Views.NeptunePage;

namespace Neptune.Web.Views.Shared
{
    public class ViewPageContentViewData
    {
        public readonly EFModels.Entities.NeptunePage NeptunePage;
        public readonly bool ShowEditButton;
        public readonly IHtmlContent HtmlContent;
        public EditViewData EditViewData { get; set; }
        public EditViewModel EditViewModel { get; set; }


        public ViewPageContentViewData(LinkGenerator linkGenerator, EFModels.Entities.NeptunePage neptunePage, Person currentPerson)
        {
            NeptunePage = neptunePage;
            ShowEditButton = new NeptunePageManageFeature().HasPermission(currentPerson, neptunePage).HasPermission;
            var htmlContent = new HtmlString(neptunePage.NeptunePageContent);
            HtmlContent = htmlContent;
            EditViewData = new EditViewData(linkGenerator, TinyMCEExtension.TinyMCEToolbarStyle.MinimalWithImages, neptunePage);
            EditViewModel = new EditViewModel(neptunePage);
        }
    }
}
