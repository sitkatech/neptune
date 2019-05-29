using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Controllers;
using HomeController = Neptune.Web.Areas.DroolTool.Controllers.HomeController;

namespace Neptune.Web.Areas.DroolTool.Views
{
    public class DroolToolModuleViewData : NeptuneViewData
    {
        public bool IsHomePage { get; }

        public DroolToolModuleViewData(Person currentPerson, NeptunePage neptunePage, bool isHomePage) : base(currentPerson, neptunePage, NeptuneArea.DroolTool)
        {
            IsHomePage = isHomePage;
            MakeDroolToolModuleMenu();
        }

        private void MakeDroolToolModuleMenu()
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.About()), CurrentPerson, "About", "Group1"),
                BuildManageMenu(CurrentPerson)
            };

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });

        }

        private LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index()), currentPerson, "Users", "Group1"));

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TrashGeneratingUnitController>(c => c.Index()), currentPerson, "Trash Generating Units", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<LandUseBlockController>(c => c.Index()), currentPerson, "Land Use Blocks", "Group3"));
            return manageMenu;
        }

    }
}