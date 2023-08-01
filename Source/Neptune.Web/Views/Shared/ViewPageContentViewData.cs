using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Shared
{
    public class ViewPageContentViewData
    {
        public readonly Models.NeptunePage NeptunePage;
        public readonly bool ShowEditButton;
        public readonly string NeptunePageContentID;
        public readonly string NeptunePageEditHoverButtonID;
        public readonly string EditPageContentUrl;

        public ViewPageContentViewData(Models.NeptunePage neptunePage, Person currentPerson)
        {
            NeptunePage = neptunePage;
            ShowEditButton = new NeptunePageManageFeature().HasPermission(currentPerson, neptunePage).HasPermission;
            NeptunePageContentID = $"neptunePageContent{neptunePage.NeptunePageID}";
            NeptunePageEditHoverButtonID = $"editHoverButton{neptunePage.NeptunePageID}";
            EditPageContentUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(t => t.EditInDialog(NeptunePage));
        }        
    }
}
