using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;
using Neptune.Web.Views.NeptunePage;

namespace Neptune.Web.Views.Shared
{
    public class ViewPageContentViewData
    {
        public readonly EFModels.Entities.NeptunePage NeptunePage;
        public readonly bool ShowEditButton;
        public readonly HtmlString HtmlContent;
        public EditViewData EditViewData { get; set; }
        public EditViewModel EditViewModel { get; set; }


        public ViewPageContentViewData(EFModels.Entities.NeptunePage neptunePage, Person currentPerson, LinkGenerator linkGenerator)
        {
            NeptunePage = neptunePage;
            ShowEditButton = true; // todo: new NeptunePageManageFeature().HasPermission(currentPerson, neptunePage).HasPermission;
            HtmlContent = new HtmlString(neptunePage.NeptunePageContent);
            EditViewData = new EditViewData(TinyMCEExtension.TinyMCEToolbarStyle.MinimalWithImages);
            EditViewModel = new EditViewModel(neptunePage);
        }
    }
}
