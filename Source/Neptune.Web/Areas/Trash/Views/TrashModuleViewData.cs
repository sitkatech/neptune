using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views;
using HomeController = Neptune.Web.Areas.Trash.Controllers.HomeController;

namespace Neptune.Web.Areas.Trash.Views
{
    public class TrashModuleViewData : NeptuneViewData
    {
        public TrashModuleViewData(Person currentPerson) : base(currentPerson)
        {
            MakeTrashModuleMenu();
        }

        private void MakeTrashModuleMenu()
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildTrashBMPsMenu(CurrentPerson),
                BuildOVTAMenu(CurrentPerson),
                BuildResultsMenu(CurrentPerson),
                BuildManageMenu(CurrentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }

        private static LtInfoMenuItem BuildTrashBMPsMenu(Person currentPerson)
        {
            var TrashBMPsMenu = new LtInfoMenuItem("Trash BMPs");

            TrashBMPsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.Index()), currentPerson, "Home", "Group1"));

            return TrashBMPsMenu;
        }

        private LtInfoMenuItem BuildOVTAMenu(Person currentPerson)
        {
            var OVTAMenu = new LtInfoMenuItem("OVTA");

            OVTAMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OnlandVisualTrashAssessmentController>(c => c.Index()), currentPerson, "View All OVTAs", "Group1"));
 

            return OVTAMenu;
        }


        private static LtInfoMenuItem BuildResultsMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index()), "Results", currentPerson.IsManagerOrAdmin(), true, null);
        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<Web.Controllers.HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group1"));
            return manageMenu;
        }

        public TrashModuleViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
            MakeTrashModuleMenu();
        }

        public TrashModuleViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, NeptunePage neptunePage) : base(currentPerson, stormwaterBreadCrumbEntity, neptunePage)
        {
            MakeTrashModuleMenu();
        }

        public TrashModuleViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            MakeTrashModuleMenu();
        }
    }
}