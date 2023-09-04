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

using Neptune.Web.Views.Shared;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

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
        public NeptunePage NeptunePage { get; }
        public Person CurrentPerson { get; }
        public string? NeptuneHomeUrl { get; }
        public string? LogInUrl { get; }
        public string? LogOutUrl { get; }
        public string? RequestSupportUrl { get; }
        public string? LegalUrl { get; }
        public ViewPageContentViewData ViewPageContentViewData { get; }
        public NeptuneNavBarViewData NeptuneNavBarViewData { get; }
        public List<LtInfoMenuItem> TopLevelLtInfoMenuItems;
        public readonly LinkGenerator LinkGenerator;
        private readonly HttpContext _httpContext;

        /// <summary>
        /// Call for page without associated NeptunePage
        /// </summary>
        protected NeptuneViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptuneArea neptuneArea) : this(httpContext, linkGenerator, currentPerson, null, neptuneArea)
        {
        }

        /// <summary>
        /// Call for page with associated NeptunePage
        /// </summary>
        protected NeptuneViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage, bool isHomePage,
            NeptuneArea neptuneArea)
        {
            NeptunePage = neptunePage;
            LinkGenerator = linkGenerator;
            _httpContext = httpContext;

            CurrentPerson = currentPerson;
            NeptuneHomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, c => c.Index());

            LogInUrl = NeptuneHelpers.GenerateLogInUrlWithReturnUrl(httpContext, linkGenerator, "");
            LogOutUrl = NeptuneHelpers.GenerateLogOutUrl();

            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(linkGenerator, c => c.Support());

            LegalUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, c => c.Legal());

            MakeNeptuneMenu(currentPerson);
            NeptuneNavBarViewData = new NeptuneNavBarViewData(currentPerson, LogInUrl, LogOutUrl, RequestSupportUrl, neptuneArea, isHomePage, linkGenerator);

            ViewPageContentViewData = neptunePage != null ? new ViewPageContentViewData(neptunePage, currentPerson) : null;
        }

        protected NeptuneViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage, NeptuneArea neptuneArea) : this(httpContext, linkGenerator, currentPerson, neptunePage, false, neptuneArea)
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

        private LtInfoMenuItem BuildDelineationMenu(Person currentPerson)
        {
            var delineationMenu = new LtInfoMenuItem("Delineation");

            //delineationMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<DelineationController>(c => c.DelineationMap(null)), currentPerson, "Delineation Map", "Group1"));
            //delineationMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<DelineationController>(c => c.DelineationReconciliationReport()), currentPerson, "Delineation Reconciliation Report", "Group1"));

            return delineationMenu;
        }

        private LtInfoMenuItem BuildBMPInventoryMenu(Person currentPerson)
        {
            var bmpMenu = new LtInfoMenuItem("BMP Inventory");

            ////bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JurisdictionController>(c => c.Index()), currentPerson, "Jurisdictions", "Group1"));

            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(LinkGenerator, c => c.FindABMP()), currentPerson, "Find a BMP", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(LinkGenerator, c => c.ViewTreatmentBMPModelingAttributes()), currentPerson, "Modeling Parameters", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(LinkGenerator, c => c.Index()), currentPerson, "View All BMPs", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(LinkGenerator, c => c.TreatmentBMPAssessmentSummary()), currentPerson, "View Latest BMP Assessments", "Group2"));

            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldVisitController>(LinkGenerator, c => c.Index()), currentPerson, "View All Field Records", "Group2"));

            //bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Index()), currentPerson, FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized(), "Group3"));
            //bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ParcelController>(c => c.Index()), currentPerson, "Parcels", "Group3"));

            return bmpMenu;
        }

        private LtInfoMenuItem BuildProgramInfoMenu(Person currentPerson)
        {
            var programInfoMenu = new LtInfoMenuItem("Program Info");

            //programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(c => c.Index()), currentPerson, "Observation Types", "Group1"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPTypeController>(LinkGenerator ,c => c.Index()), currentPerson, "Treatment BMP Types", "Group1"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FundingSourceController>(LinkGenerator, c => c.Index()), currentPerson, "Funding Sources", "Group1"));

            if (new JurisdictionEditFeature().HasPermissionByPerson(currentPerson))
            {
                programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(
                new SitkaRoute<WebServicesController>(c => c.Index(), SitkaRouteSecurity.SSL, LinkGenerator), currentPerson, "Web Services", "Group 2"));
            }

            return programInfoMenu;
        }


        private LtInfoMenuItem BuildDashboardMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(LinkGenerator, c => c.Index()), "Dashboard", currentPerson.IsManagerOrAdmin(), true, null);
        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(LinkGenerator, c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(LinkGenerator, c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(LinkGenerator, c => c.Index()), currentPerson, "Users", "Group1"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(LinkGenerator, c => c.Index()), currentPerson, $"Organizations", "Group1"));

            //if (currentPerson.IsAdministrator())
            //{
            //    manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JurisdictionController>(c => c.Index()), currentPerson, "Jurisdictions", "Group1"));
            //}

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(LinkGenerator,c => c.Manage()), currentPerson, "Observation Types", "Group2"));
            if (!currentPerson.IsAnonymousOrUnassigned())
            {
                manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(
                    new SitkaRoute<TreatmentBMPTypeController>(LinkGenerator, c => c.Manage()), currentPerson, "Treatment BMP Types",
                    "Group2"));
            }
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomAttributeTypeController>(c => c.Manage()), currentPerson, "Custom Attributes", "Group2"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TrashGeneratingUnitController>(c => c.Index()), currentPerson, "Trash Generating Units", "Group3"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<LandUseBlockController>(c => c.Index()), currentPerson, "Land Use Blocks", "Group3"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HRUCharacteristicController>(c => c.Index()), currentPerson, "HRU Characteristics", "Group4"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c => c.Index()), currentPerson, "Regional Subbasins", "Group4"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinController>(c => c.Grid()), currentPerson, "Regional Subbasin Grid", "Group4"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<RegionalSubbasinRevisionRequestController>(c => c.Index()), currentPerson, "Regional Subbasin Revision Requests", "Group4"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WaterQualityManagementPlanController>(c => c.LGUAudit()), currentPerson, "Water Quality Management Plan LGU Audit", "Group5"));

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
