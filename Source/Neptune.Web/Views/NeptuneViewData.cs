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
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views
{
    public abstract class NeptuneViewData
    {
        public List<LtInfoMenuItem> TopLevelLtInfoMenuItems;

        public readonly string FullProjectListUrl;
        public readonly string ProjectSearchUrl;
        public readonly string ProjectFindUrl;
        public string PageTitle;
        public string HtmlPageTitle;
        public string BreadCrumbTitle;
        public string EntityName;
        public readonly Models.NeptunePage NeptunePage;
        public readonly Person CurrentPerson;
        public readonly string NeptuneHomeUrl;
        public readonly string LogInUrl;
        public readonly string LogOutUrl;
        public readonly string RequestSupportUrl;
        public readonly ViewPageContentViewData ViewPageContentViewData;

        /// <summary>
        /// Call for page without associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson) : this(currentPerson, null)
        {
        }
     
        /// <summary>
        /// Call for page with associated NeptunePage
        /// </summary>
        protected NeptuneViewData(Person currentPerson, Models.NeptunePage neptunePage)
        {
            NeptunePage = neptunePage;

            CurrentPerson = currentPerson;
            NeptuneHomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index());

            LogInUrl = NeptuneHelpers.GenerateLogInUrlWithReturnUrl();
            LogOutUrl = NeptuneHelpers.GenerateLogOutUrlWithReturnUrl();

            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.Support());

            MakeNeptuneMenu(currentPerson);

            ViewPageContentViewData = neptunePage != null ? new ViewPageContentViewData(neptunePage, currentPerson) : null;
        }


        private void MakeNeptuneMenu(Person currentPerson)
        {
            var homeMenuItem = LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.Index()), currentPerson, "Home");

            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                homeMenuItem,
                BuildAboutMenu(currentPerson),
                BuildProgramInfoMenu(currentPerson),
                //BuildResultsMenu(currentPerson),
                BuildManageMenu(currentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });
        }

        private static LtInfoMenuItem BuildAboutMenu(Person currentPerson)
        {
            var aboutMenu = new LtInfoMenuItem("About");
            aboutMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.About()), currentPerson, "About " + MultiTenantHelpers.GetTenantDisplayName()));
            return aboutMenu;
        }

        private static LtInfoMenuItem BuildProgramInfoMenu(Person currentPerson)
        {
            var programInfoMenu = new LtInfoMenuItem("Program Info");
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group3"));

            return programInfoMenu;
        }


        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group1"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<NeptunePageController>(c => c.Index()), currentPerson, "Page Content", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group2"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TenantController>(c => c.Detail()), currentPerson, "Tenant Configuration", "Group3"));

            return manageMenu;
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
            else if (!string.IsNullOrWhiteSpace(PageTitle))
            {
                return $" | {PageTitle}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
