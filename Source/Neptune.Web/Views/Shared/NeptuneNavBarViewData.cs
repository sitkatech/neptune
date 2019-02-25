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
using LtInfo.Common.ModalDialog;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;

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
        public readonly string HomeUrl;

        public readonly NeptuneArea NeptuneArea;
        public readonly bool ShowLinkToArea;

        public NeptuneNavBarViewData(Person currentPerson, string logInUrl, string logOutUrl, string requestSupportUrl, NeptuneArea neptuneArea, bool isHomePage)
        {
            CurrentPerson = currentPerson;

            HomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(hc => hc.Index());

            LogInUrl = logInUrl;
            LogOutUrl = logOutUrl;
            RequestSupportUrl = requestSupportUrl;

            AboutUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(hc => hc.About());
            TopLevelNeptuneMenus = MakeNeptuneHelpMenu(currentPerson);

            NeptuneArea = neptuneArea;
            ShowLinkToArea = !isHomePage;
        }

        private List<LtInfoMenuItem> MakeNeptuneHelpMenu(Person currentPerson)
        {
            var helpMenu = new LtInfoMenuItem("Help");
            helpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HelpController>(c=>c.Support()), currentPerson, "Request Support", "ToolHelp"));
            helpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.Training()), currentPerson, "Training", "ToolHelp"));

            helpMenu.ExtraDropdownMenuCssClasses = new List<string> { "dropdown-menu-right" };
            helpMenu.ExtraTopLevelMenuCssClasses = new List<string> { "topRightMenu noHighlight" };
            helpMenu.ExtraTopLevelListItemCssClasses = new List<string> { "topRightMenuParent" };

            var topLevelLtInfoMenuItems = new List<LtInfoMenuItem> { helpMenu };

            return topLevelLtInfoMenuItems;
        }

    }
}
