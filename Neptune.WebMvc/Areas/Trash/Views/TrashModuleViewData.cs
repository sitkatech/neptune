using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views;
using HomeController = Neptune.WebMvc.Areas.Trash.Controllers.HomeController;

namespace Neptune.WebMvc.Areas.Trash.Views
{
    public class TrashModuleViewData : NeptuneViewData
    {
        public TrashModuleViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.Trash, webConfiguration)
        {
            MakeTrashModuleMenu();
        }
        public TrashModuleViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.Trash, webConfiguration)
        {
            MakeTrashModuleMenu();
        }

        private void MakeTrashModuleMenu()
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildOVTAMenu(CurrentPerson),
                BuildResultsMenu(CurrentPerson)
            };

            if (!CurrentPerson.IsAnonymousOrUnassigned())
            {
                TopLevelLtInfoMenuItems.Add(LtInfoMenuItem.MakeItem(
                    new SitkaRoute<DelineationController>(LinkGenerator, x => x.DelineationMap(null)), CurrentPerson,
                    "Delineation Map", "Group1"));
                TopLevelLtInfoMenuItems.Add(BuildManageMenu(CurrentPerson));
            }

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });
        }

        private LtInfoMenuItem BuildOVTAMenu(Person currentPerson)
        {
            var ovtaMenu = new LtInfoMenuItem("OVTA");

            ovtaMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OnlandVisualTrashAssessmentController>(LinkGenerator, x => x.Index()), currentPerson, "View All OVTAs", "Group1"));
            
            return ovtaMenu;
        }

        private LtInfoMenuItem BuildResultsMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<HomeController>.BuildUrlFromExpression(LinkGenerator, x => x.Index()), "Results", true, true, null);
        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WebMvc.Controllers.HomeController>(LinkGenerator, x => x.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(LinkGenerator, x => x.Index()), currentPerson, "Page Content", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(LinkGenerator, x => x.Index()), currentPerson, "Custom Labels & Definitions", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(LinkGenerator, x => x.Index()), currentPerson, "Users", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(LinkGenerator, x => x.Index()), currentPerson, $"{FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized()}", "Group1"));


            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TrashGeneratingUnitController>(LinkGenerator, x => x.Index()), currentPerson, "Trash Generating Units", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<LandUseBlockController>(LinkGenerator, x => x.Index()), currentPerson, "Land Use Blocks", "Group3"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HRUCharacteristicController>(LinkGenerator, x => x.Index()), currentPerson, "HRU Characteristics", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(LinkGenerator, x => x.Index()), currentPerson, "Regional Subbasins", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(LinkGenerator, x => x.Grid()), currentPerson, "Regional Subbasin Grid", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinRevisionRequestController>(LinkGenerator, x => x.Index()), currentPerson, "Regional Subbasin Revision Requests", "Group4"));

            return manageMenu;
        }
    }
}
