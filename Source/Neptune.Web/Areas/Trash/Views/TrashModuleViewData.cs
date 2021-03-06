﻿using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Modeling.Controllers;
using HomeController = Neptune.Web.Areas.Trash.Controllers.HomeController;

namespace Neptune.Web.Areas.Trash.Views
{
    public class TrashModuleViewData : NeptuneViewData
    {
        public TrashModuleViewData(Person currentPerson) : base(currentPerson, NeptuneArea.Trash)
        {
            MakeTrashModuleMenu();
        }

        public TrashModuleViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.Trash)
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
                    new SitkaRoute<DelineationController>(c => c.DelineationMap(null)), CurrentPerson,
                    "Delineation Map", "Group1"));
                TopLevelLtInfoMenuItems.Add(BuildManageMenu(CurrentPerson));
            }

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });
        }

        private LtInfoMenuItem BuildOVTAMenu(Person currentPerson)
        {
            var ovtaMenu = new LtInfoMenuItem("OVTA");

            ovtaMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OnlandVisualTrashAssessmentController>(c => c.Index()), currentPerson, "View All OVTAs", "Group1"));
            
            return ovtaMenu;
        }

        private static LtInfoMenuItem BuildResultsMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index()), "Results", true, true, null);
        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<Web.Controllers.HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group1"));


            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TrashGeneratingUnitController>(c => c.Index()), currentPerson, "Trash Generating Units", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<LandUseBlockController>(c => c.Index()), currentPerson, "Land Use Blocks", "Group3"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HRUCharacteristicController>(c => c.Index()), currentPerson, "HRU Characteristics", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c => c.Index()), currentPerson, "Regional Subbasins", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c => c.Grid()), currentPerson, "Regional Subbasin Grid", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinRevisionRequestController>(c => c.Index()), currentPerson, "Regional Subbasin Revision Requests", "Group4"));

            return manageMenu;
        }
    }
}
