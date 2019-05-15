﻿using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.DroolTool.Controllers;

namespace Neptune.Web.Areas.DroolTool.Views
{
    public class DroolToolModuleViewData : NeptuneViewData
    {
        public DroolToolModuleViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.DroolTool)
        {
            MakeDroolToolModuleMenu();
        }

        private void MakeDroolToolModuleMenu()
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.About()), CurrentPerson, "About", "Group1"),
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }
    }
}