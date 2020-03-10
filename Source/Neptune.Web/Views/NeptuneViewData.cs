/*-----------------------------------------------------------------------
<copyright file="NeptuneViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Areas.Trash.Controllers;
using HomeController = Neptune.Web.Controllers.HomeController;
using ParcelController = Neptune.Web.Controllers.ParcelController;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Views
{
    public abstract class NeptuneViewData
    {
        public string PageTitle { get; set; }
        public string HtmlPageTitle { get; set; }
        public string BreadCrumbTitle { get; set; }
        public string EntityName { get; set; }
        public string EntityUrl { get; set; }
        public string SubEntityName { get; set; }
        public string SubEntityUrl { get; set; }
        public Models.NeptunePage NeptunePage { get; }
        public Person CurrentPerson { get; }
        public string NeptuneHomeUrl { get; }
        public string LogInUrl { get; }
        public string LogOutUrl { get; }
        public string RequestSupportUrl { get; }
        public string LegalUrl { get; }
        public ViewPageContentViewData ViewPageContentViewData { get; }
        public NeptuneNavBarViewData NeptuneNavBarViewData { get; }
        public List<LtInfoMenuItem> TopLevelLtInfoMenuItems;

        /// <summary>
        /// Call for page without associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson, NeptuneArea neptuneArea) : this(currentPerson, null, neptuneArea)
        {
        }

        /// <summary>
        /// Call for page with associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson, Models.NeptunePage neptunePage, bool isHomePage,
            NeptuneArea neptuneArea)
        {
            NeptunePage = neptunePage;

            CurrentPerson = currentPerson;
            NeptuneHomeUrl = SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(c => c.Index(), NeptuneWebConfiguration.CanonicalHostNameRoot);

            LogInUrl = NeptuneHelpers.GenerateLogInUrlWithReturnUrl();
            LogOutUrl = NeptuneHelpers.GenerateLogOutUrlWithReturnUrl();

            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.Support());

            LegalUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Legal());

            MakeNeptuneMenu(currentPerson);
            NeptuneNavBarViewData = new NeptuneNavBarViewData(currentPerson, LogInUrl, LogOutUrl, RequestSupportUrl, neptuneArea, isHomePage);

            ViewPageContentViewData = neptunePage != null ? new ViewPageContentViewData(neptunePage, currentPerson) : null;
        }

        protected NeptuneViewData(Person currentPerson, Models.NeptunePage neptunePage, NeptuneArea neptuneArea) : this(currentPerson,
            neptunePage, false, neptuneArea)
        {

        }

        private void MakeNeptuneMenu(Person currentPerson)
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildBMPInventoryMenu(currentPerson),
                BuildProgramInfoMenu(currentPerson),
                BuildDashboardMenu(currentPerson),
                BuildDelineationMenu(currentPerson),
                BuildManageMenu(CurrentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }

        private static LtInfoMenuItem BuildDelineationMenu(Person currentPerson)
        {
            var delineationMenu = new LtInfoMenuItem("Delineation");

            delineationMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<DelineationController>(c => c.DelineationMap(null)), currentPerson, "Delineation Map", "Group1"));
            delineationMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<DelineationController>(c => c.DelineationReconciliationReport()), currentPerson, "Delineation Reconciliation Report", "Group1"));
            
            return delineationMenu;
        }

        private static LtInfoMenuItem BuildBMPInventoryMenu(Person currentPerson)
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
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Index()), currentPerson, Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized(), "Group3"));
            }
            if (new JurisdictionManageFeature().HasPermissionByPerson(currentPerson))
            {
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ParcelController>(c => c.Index()), currentPerson, "Parcels", "Group3"));
            }

            return bmpMenu;
        }

        private static LtInfoMenuItem BuildProgramInfoMenu(Person currentPerson)
        {
            var programInfoMenu = new LtInfoMenuItem("Program Info");

            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(c => c.Index()), currentPerson, "Observation Types", "Group1"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPTypeController>(c => c.Index()), currentPerson, "Treatment BMP Types", "Group1"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FundingSourceController>(c => c.Index()), currentPerson, Models.FieldDefinition.FundingSource.GetFieldDefinitionLabelPluralized(), "Group1"));

            return programInfoMenu;
        }


        private static LtInfoMenuItem BuildDashboardMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(c => c.Index()), "Dashboard", currentPerson.IsManagerOrAdmin(), true, null);
        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group1"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(c => c.Manage()), currentPerson, "Observation Types", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPTypeController>(c => c.Manage()), currentPerson, "Treatment BMP Types", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomAttributeTypeController>(c => c.Manage()), currentPerson, "Custom Attributes", "Group2"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TrashGeneratingUnitController>(c=>c.Index()), currentPerson, "Trash Generating Units", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<LandUseBlockController>(c=>c.Index()), currentPerson, "Land Use Blocks", "Group3"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HRUCharacteristicController>(c=>c.Index()), currentPerson, "HRU Characteristics", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c=>c.Index()), currentPerson, "Regional Subbasins", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c=>c.Grid()), currentPerson, "Regional Subbasin Grid", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinRevisionRequestController>(c => c.Index()), currentPerson, "Regional Subbasin Revision Requests", "Group4"));

            return manageMenu;
        }

        public string GetBreadCrumbTitle()
        {
            if (!string.IsNullOrWhiteSpace(BreadCrumbTitle))
            {
                return $" | {BreadCrumbTitle}";
            }

            return !string.IsNullOrWhiteSpace(PageTitle)
                ? $" | {PageTitle}"
                : string.Empty;
        }
    }
}
