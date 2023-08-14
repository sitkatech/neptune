/*-----------------------------------------------------------------------
<copyright file="LtInfoMenuItem.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;


namespace Neptune.Web.Views
{
    public class LtInfoMenuItem
    {
        private const string Indent = "    ";
        public List<string> ExtraTopLevelMenuCssClasses = new();
        public List<string> ExtraDropdownMenuCssClasses = new();
        public List<string> ExtraTopLevelListItemCssClasses = new();
        public readonly string RawString;

        /// <summary>
        /// Make a NeptuneMenuItem from a route. A feature is required on the Route and will be used to check access for the menu item.
        /// If menu item is not accessible, it will not be shown.
        /// </summary>
        public static LtInfoMenuItem MakeItem<T>(SitkaRoute<T> route, Person currentPerson, string menuItemName) where T : Controller
        {
            return MakeItem(route, currentPerson, menuItemName, null);
        }

        /// <summary>
        /// Make a NeptuneMenuItem from a route. A feature is required on the Route and will be used to check access for the menu item.
        /// If menu item is not accessible, it will not be shown.
        /// </summary>
        public static LtInfoMenuItem MakeItem<T>(SitkaRoute<T> route, Person currentPerson, string menuItemName, string menuGroupName) where T : Controller
        {
            var urlString = route.BuildUrlFromExpression();
            var shouldShow = true; // todo: NeptuneBaseFeature.IsAllowed(route, currentPerson);
            return new LtInfoMenuItem(urlString, menuItemName, shouldShow, false, menuGroupName);
        }

        public static LtInfoMenuItem MakeItem(string menuItemName, string rawString, string menuGroupName)
        {
            return new LtInfoMenuItem(menuItemName, rawString, menuGroupName);
        }


        /// <summary>
        /// Manual construction of a LtInfoMenuItem with no children
        /// Only public for unit testing
        /// </summary>
        public LtInfoMenuItem(string urlString, string menuItemName, bool shouldShow, bool isTopLevelMenu, string menuGroupName)
        {
            MenuGroupName = menuGroupName;
            MenuItemName = menuItemName;
            UrlString = urlString;
            ShouldShow = shouldShow;
            IsTopLevelMenuItem = isTopLevelMenu;
            ChildMenus = new List<LtInfoMenuItem>();
        }

        /// <summary>
        /// Manual construction of a LtInfoMenuItem with no children and no url
        /// Use case is a top level menu item
        /// </summary>
        public LtInfoMenuItem(string menuItemName) : this(null, menuItemName, true, true, null)
        {
        }

        public void AddMenuItem(LtInfoMenuItem menuItemToAdd)
        {
            Check.Require(!HasUrl, "You cannot add children to a menu item that is a link!");
            ChildMenus.Add(menuItemToAdd);
        }

        private bool HasUrl
        {
            get { return !string.IsNullOrWhiteSpace(UrlString); }
        }

        private IEnumerable<LtInfoMenuItem> ChildrenMenuItemsSecurityFiltered
        {
            get { return ChildMenus.Where(mi => mi.HasUrl && mi.ShouldShow).ToList(); }
        }

        private IEnumerable<LtInfoMenuItem> ChildenMenuItemsAndDividersSecurityFiltered
        {
            get { return ChildMenus.Where(mi => mi.ShouldShow).ToList(); }
        }

        public HtmlString RenderMenu()
        {
            return new HtmlString(RenderMenu(Indent));
        }

        public string RenderMenu(string indent)
        {
            // Example:
            //    <li><a href="@ViewDataTyped.HomeUrl">Home</a></li>
            //    <li><a href="@ViewDataTyped.OverviewUrl">About</a>
            //        <ul>
            //            <li><a href="@ViewDataTyped.OverviewUrl">Overview</a></li>
            //            <li><a href="@ViewDataTyped.HistoryUrl">History</a></li>
            //            <li><a href="@ViewDataTyped.PartnersUrl">Partners</a></li>
            //            <li><a href="@ViewDataTyped.FaqUrl">FAQ</a></li>
            //        </ul>
            //    </li>  
            if (RawString != null)
            {
                return $"<li>{RawString}</li>";
            }
            if (!ShouldShow || (IsTopLevelMenuItem && !HasUrl && !ChildrenMenuItemsSecurityFiltered.Any()))
            {
                return string.Empty;
            }
            if (ChildrenMenuItemsSecurityFiltered.Any())
            {
                return RenderMenuWithChildren(indent);
            }

            var extraCssClassesDictionary = ExtraTopLevelMenuCssClasses.Any() ? new Dictionary<string, string> {{"class", string.Join(" ", ExtraTopLevelMenuCssClasses)}} : null;
            var anchorTagString = UrlTemplate.MakeHrefString(UrlString, MenuItemName, extraCssClassesDictionary);
            return $"{indent}<li class=\"\">{anchorTagString}</li>";
        }

        private string RenderMenuWithChildren(string indent)
        {
            var childMenuItems = new List<string>();
            var childIndent = $"{Indent}{indent}";
            var classes = ExtraDropdownMenuCssClasses.Count != 0 ? $"dropdown-menu {String.Join(" ", ExtraDropdownMenuCssClasses)}" : "dropdown-menu";
            childMenuItems.Add($"{childIndent}<ul class=\"{classes}\" role=\"menu\">");

            var menuGroups = ChildenMenuItemsAndDividersSecurityFiltered.GroupBy(x => x.MenuGroupName).ToList();
            var currentIndent = string.Format("{0}{1}", Indent, childIndent);
            foreach (var menuGroup in menuGroups)
            {
                childMenuItems.AddRange(menuGroup.Select(childMenuItem => childMenuItem.RenderMenu(currentIndent)).ToList());
                if (menuGroups.Count > 1 && menuGroup != menuGroups.Last())
                {
                    childMenuItems.Add(CreateDivider(currentIndent));
                }
            }

            childMenuItems.Add($"{childIndent}</ul>");

            var childMenuItemCssClasses = "dropdown-toggle";
            if (ExtraTopLevelMenuCssClasses.Any())
            {
                childMenuItemCssClasses += " " + string.Join(" ", ExtraTopLevelMenuCssClasses);
            }

            var liClasses = ExtraTopLevelListItemCssClasses?.Any() ?? false
                ? " " + string.Join(" ", ExtraTopLevelListItemCssClasses) // super pedantic, but front pad the space so we don't include it if it doesn't need to be there
                : string.Empty;


            return string.Format(@"{0}<li {1}>
{0}<a href=""#"" class=""{2}"" data-toggle=""dropdown"" role=""button"" aria-expanded=""false"">{3} <span class=""glyphicon glyphicon-menu-down""></span></a>
{4}
{0}</li>", indent, $"class=\"dropdown{liClasses}\"", childMenuItemCssClasses, MenuItemName,
                $"{string.Join("\r\n", childMenuItems)}\r\n{indent}");
        }

        private static string CreateDivider(string indent)
        {
            return $"{indent}<li class=\"divider\"></li>";
        }

        private LtInfoMenuItem(string menuItemName, string rawstring, string menuGroupName)
        {
            MenuItemName = menuItemName;
            RawString = rawstring;
            ShouldShow = true;
            MenuGroupName = menuGroupName;
        }

        public readonly string MenuGroupName;
        public readonly string MenuItemName;
        public readonly string UrlString;
        public readonly bool ShouldShow;
        public readonly bool IsTopLevelMenuItem;
        public readonly List<LtInfoMenuItem> ChildMenus;
    }
}
