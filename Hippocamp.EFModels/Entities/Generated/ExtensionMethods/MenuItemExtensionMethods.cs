//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MenuItem]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class MenuItemExtensionMethods
    {
        public static MenuItemDto AsDto(this MenuItem menuItem)
        {
            var menuItemDto = new MenuItemDto()
            {
                MenuItemID = menuItem.MenuItemID,
                MenuItemName = menuItem.MenuItemName,
                MenuItemDisplayName = menuItem.MenuItemDisplayName
            };
            DoCustomMappings(menuItem, menuItemDto);
            return menuItemDto;
        }

        static partial void DoCustomMappings(MenuItem menuItem, MenuItemDto menuItemDto);

        public static MenuItemSimpleDto AsSimpleDto(this MenuItem menuItem)
        {
            var menuItemSimpleDto = new MenuItemSimpleDto()
            {
                MenuItemID = menuItem.MenuItemID,
                MenuItemName = menuItem.MenuItemName,
                MenuItemDisplayName = menuItem.MenuItemDisplayName
            };
            DoCustomSimpleDtoMappings(menuItem, menuItemSimpleDto);
            return menuItemSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MenuItem menuItem, MenuItemSimpleDto menuItemSimpleDto);
    }
}