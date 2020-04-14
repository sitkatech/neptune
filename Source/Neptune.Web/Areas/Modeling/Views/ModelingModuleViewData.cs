using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Security;
using HomeController = Neptune.Web.Controllers.HomeController;
using ParcelController = Neptune.Web.Controllers.ParcelController;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Areas.Modeling.Views
{
    public class ModelingModuleViewData : NeptuneViewData
    {
        public ModelingModuleViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.Modeling)
        {
            MakeTrashModuleMenu();
        }

        private void MakeTrashModuleMenu()
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildBMPInventoryMenu(CurrentPerson),
                LtInfoMenuItem.MakeItem(new SitkaRoute<DelineationController>(c => c.DelineationMap(null)), CurrentPerson, "Delineation Map", "Group1"),
                BuildManageMenu(CurrentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
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

        private LtInfoMenuItem BuildBMPInventoryMenu(Person currentPerson)
        {
            var bmpMenu = new LtInfoMenuItem("BMP Inventory");

            //bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JurisdictionController>(c => c.Index()), currentPerson, "Jurisdictions", "Group1"));

            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.FindABMP()), currentPerson, "Find a BMP", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.ViewTreatmentBMPModelingAttributes()), currentPerson, "Modeling Parameters", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.Index()), currentPerson, "View All BMPs", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.TreatmentBMPAssessmentSummary()), currentPerson, "View Latest BMP Assessments", "Group2"));

            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldVisitController>(c => c.Index()), currentPerson, "View All Field Records", "Group2"));
            if (new WaterQualityManagementPlanViewFeature().HasPermissionByPerson(currentPerson))
            {
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Index()), currentPerson, FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized(), "Group3"));
            }
            if (new JurisdictionManageFeature().HasPermissionByPerson(currentPerson))
            {
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ParcelController>(c => c.Index()), currentPerson, "Parcels", "Group3"));
            }

            return bmpMenu;
        }
    }
}