using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Shared
{
    public class ViewPageContentViewData
    {
        public readonly EFModels.Entities.NeptunePage NeptunePage;
        public readonly bool ShowEditButton;
        public readonly string NeptunePageContentID;
        public readonly string NeptunePageEditHoverButtonID;
        public readonly string EditPageContentUrl;
        public readonly HtmlString HtmlContent;

        public ViewPageContentViewData(EFModels.Entities.NeptunePage neptunePage, Person currentPerson)
        {
            NeptunePage = neptunePage;
            ShowEditButton = true; //todo: new NeptunePageManageFeature().HasPermission(currentPerson, neptunePage).HasPermission;
            NeptunePageContentID = $"neptunePageContent{neptunePage.NeptunePageID}";
            NeptunePageEditHoverButtonID = $"editHoverButton{neptunePage.NeptunePageID}";
            EditPageContentUrl = "";// todo: SitkaRoute<NeptunePageController>.BuildUrlFromExpression(t => t.EditInDialog(NeptunePage));
            HtmlContent = new HtmlString(neptunePage.NeptunePageContent);
        }        
    }
}
