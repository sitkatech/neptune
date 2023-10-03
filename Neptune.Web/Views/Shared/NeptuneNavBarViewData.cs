/*-----------------------------------------------------------------------
<copyright file="NeptuneNavBarViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Shared
{
    public class NeptuneNavBarViewData
    {
        public Person CurrentPerson { get; }
        public string RequestSupportUrl { get; }
        public string AboutUrl { get; }
        public string LogInUrl { get; }
        public string LogOutUrl { get; }
        public List<LtInfoMenuItem> TopLevelNeptuneMenus { get; }
        public IEnumerable<NeptuneArea> NeptuneAreas { get; }
        public string CurrentPersonDetailUrl { get; }
        public string PlanningModuleUrl { get; }
        public readonly string HomeUrl;

        public NeptuneArea NeptuneArea { get; }
        private LinkGenerator _linkGenerator { get; }
        public bool ShowLinkToArea { get; }

        public NeptuneNavBarViewData(LinkGenerator linkGenerator, Person currentPerson, string logInUrl,
            string logOutUrl, string requestSupportUrl,
            NeptuneArea neptuneArea, bool isHomePage, string planningModuleUrl)
        {
            CurrentPerson = currentPerson;
            _linkGenerator = linkGenerator;

            CurrentPersonDetailUrl = SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(currentPerson.PersonID));
            HomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, hc => hc.Index());

            LogInUrl = logInUrl;
            LogOutUrl = logOutUrl;
            RequestSupportUrl = requestSupportUrl;

            AboutUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, hc => hc.About());
            TopLevelNeptuneMenus = MakeNeptuneHelpMenu(currentPerson);

            NeptuneAreas = NeptuneArea.All.Where(x => x.ShowOnPrimaryNavigation).OrderBy(x => x.NeptuneAreaDisplayName);
            NeptuneArea = neptuneArea;
            PlanningModuleUrl = planningModuleUrl;
            ShowLinkToArea = !isHomePage;
        }

        private List<LtInfoMenuItem> MakeNeptuneHelpMenu(Person currentPerson)
        {
            var helpMenu = new LtInfoMenuItem("Help");

            helpMenu.AddMenuItem(LtInfoMenuItem.MakeItem("Request Support",
                $@"<a href='{RequestSupportUrl}' target='_blank'>Request Support<span class='glyphicon glyphicon-new-window'></span></a>", "ToolHelp"));

            helpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(_linkGenerator, x => x.Training()), currentPerson, "Training", "ToolHelp"));
            if (!currentPerson.IsAnonymousUser())
            {
                helpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HelpController>(_linkGenerator, x => x.BulkUploadRequest()),
                    currentPerson, "Bulk Upload Request", "ToolHelp"));
            }

            helpMenu.ExtraDropdownMenuCssClasses = new List<string> { "dropdown-menu-right" };
            helpMenu.ExtraTopLevelMenuCssClasses = new List<string> { "topRightMenu noHighlight" };
            helpMenu.ExtraTopLevelListItemCssClasses = new List<string> { "topRightMenuParent" };

            var topLevelLtInfoMenuItems = new List<LtInfoMenuItem> { helpMenu };

            return topLevelLtInfoMenuItems;
        }

        public string GetNeptuneAreaHomeUrl(NeptuneArea neptuneArea)
        {
            var neptuneAreaEnum = neptuneArea.ToEnum;
            switch (neptuneAreaEnum)
            {
                case NeptuneAreaEnum.Planning:
                    return PlanningModuleUrl;
                case NeptuneAreaEnum.Trash:
                case NeptuneAreaEnum.OCStormwaterTools:
                case NeptuneAreaEnum.Modeling:
                    return neptuneArea.NeptuneAreaName;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
