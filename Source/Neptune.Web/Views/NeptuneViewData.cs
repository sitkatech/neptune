﻿/*-----------------------------------------------------------------------
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
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.ModalDialog;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;


namespace Neptune.Web.Views
{
    public abstract class NeptuneViewData
    {
        public List<LtInfoMenuItem> TopLevelLtInfoMenuItems;

        public string PageTitle;
        public string HtmlPageTitle;
        public string BreadCrumbTitle;
        public string EntityName;
        public string EntityUrl;
        public string SubEntityName;
        public string SubEntityUrl;
        public readonly Models.NeptunePage NeptunePage;
        public readonly Person CurrentPerson;
        public readonly string NeptuneHomeUrl;
        public readonly string LogInUrl;
        public readonly string LogOutUrl;
        public readonly string RequestSupportUrl;
        public readonly string LegalUrl;
        public readonly ViewPageContentViewData ViewPageContentViewData;
        public LtInfoMenuItem HelpMenu { get; private set; }
        public NeptuneSiteExplorerViewData NeptuneSiteExplorerViewData { get; }
        public NeptuneNavBarViewData NeptuneNavBarViewData { get; }

        /// <summary>
        /// Call for page without associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson) : this(currentPerson, null, null)
        {
        }
     
        /// <summary>
        /// Call for page with associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson, Models.NeptunePage neptunePage)
        {
            NeptunePage = neptunePage;

            CurrentPerson = currentPerson;
            NeptuneHomeUrl = SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(c => c.Index(), NeptuneWebConfiguration.CanonicalHostNameRoot);

            LogInUrl = NeptuneHelpers.GenerateLogInUrlWithReturnUrl();
            LogOutUrl = NeptuneHelpers.GenerateLogOutUrlWithReturnUrl();

            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.Support());

            LegalUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Legal());

            NeptuneSiteExplorerViewData = new NeptuneSiteExplorerViewData(currentPerson, NeptuneArea.OCStormwaterTools); // todo: this should be passed from the constructor
           NeptuneNavBarViewData = new NeptuneNavBarViewData(currentPerson, LogInUrl, LogOutUrl, RequestSupportUrl);

            MakeNeptuneMenu(currentPerson);

            ViewPageContentViewData = neptunePage != null ? new ViewPageContentViewData(neptunePage, currentPerson) : null;
        }

        protected NeptuneViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.NeptunePage neptunePage) : this(currentPerson, neptunePage)
        {
        }

        protected NeptuneViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity) : this(currentPerson)
        {
        }

        private void MakeNeptuneMenu(Person currentPerson)
        {
            var aboutMenuItem = LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.About()), currentPerson, "About");

            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildBMPInventoryMenu(currentPerson),
                BuildProgramInfoMenu(currentPerson),
                BuildDashboardMenu(currentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }

        private static LtInfoMenuItem BuildBMPInventoryMenu(Person currentPerson)
        {
            var bmpMenu = new LtInfoMenuItem("BMP Inventory");

            //bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JurisdictionController>(c => c.Index()), currentPerson, "Jurisdictions", "Group1"));

            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.FindABMP()), currentPerson, "Find a BMP", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPController>(c => c.Index()), currentPerson, "View All BMPs", "Group1"));
            bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldVisitController>(c => c.Index()), currentPerson, "Field Records", "Group1"));

            if (new WaterQualityManagementPlanViewFeature().HasPermissionByPerson(currentPerson))
            {
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Index()), currentPerson, Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized(), "Group1"));
            }
            if (new JurisdictionManageFeature().HasPermissionByPerson(currentPerson))
            {
                bmpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ParcelController>(c => c.Index()), currentPerson, "Parcels", "Group1"));
            }


            return bmpMenu;
        }

        private LtInfoMenuItem BuildProgramInfoMenu(Person currentPerson)
        {
            var programInfoMenu = new LtInfoMenuItem("Manage");

            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(c => c.Index()), currentPerson, "Observation Types", "Group1"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPTypeController>(c => c.Index()), currentPerson, "Treatment BMP Types", "Group1"));

            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FundingSourceController>(c => c.Index()), currentPerson, Models.FieldDefinition.FundingSource.GetFieldDefinitionLabelPluralized(), "Group1"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group2"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group2"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group3"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group3"));

            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(c => c.Manage()), currentPerson, "Observation Types", "Group4"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TreatmentBMPTypeController>(c => c.Manage()), currentPerson, "Treatment BMP Types", "Group4"));
            //manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomAttributeTypeController>(c => c.Manage()), currentPerson, "Custom Attributes", "Group4"));

            return programInfoMenu;
        }


        private static LtInfoMenuItem BuildDashboardMenu(Person currentPerson)
        {
            return new LtInfoMenuItem(SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(c => c.Index()), "Dashboard", currentPerson.IsManagerOrAdmin(), true, null);
        }

        public string IsActiveUrl(string currentUrlPathAndQuery, string urlToCompare)
        {
            return currentUrlPathAndQuery == urlToCompare ? " class=\"active\"" : string.Empty;
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
