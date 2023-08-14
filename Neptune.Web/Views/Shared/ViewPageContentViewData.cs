using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Shared
{
    public class ViewPageContentViewData
    {
        public readonly NeptunePage NeptunePage;
        public readonly bool ShowEditButton;
        public readonly string NeptunePageContentID;
        public readonly string NeptunePageEditHoverButtonID;
        public readonly string EditPageContentUrl;

        public ViewPageContentViewData(NeptunePage neptunePage, Person currentPerson)
        {
            NeptunePage = neptunePage;
            ShowEditButton = true; //new NeptunePageManageFeature().HasPermission(currentPerson, neptunePage).HasPermission;
            NeptunePageContentID = $"neptunePageContent{neptunePage.NeptunePageID}";
            NeptunePageEditHoverButtonID = $"editHoverButton{neptunePage.NeptunePageID}";
            EditPageContentUrl = "";// todo: SitkaRoute<NeptunePageController>.BuildUrlFromExpression(t => t.EditInDialog(NeptunePage));
        }        
    }
}
