//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomPage]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class CustomPageExtensionMethods
    {
        public static CustomPageDto AsDto(this CustomPage customPage)
        {
            var customPageDto = new CustomPageDto()
            {
                CustomPageID = customPage.CustomPageID,
                CustomPageDisplayName = customPage.CustomPageDisplayName,
                CustomPageVanityUrl = customPage.CustomPageVanityUrl,
                CustomPageContent = customPage.CustomPageContent,
                MenuItem = customPage.MenuItem.AsDto(),
                SortOrder = customPage.SortOrder
            };
            DoCustomMappings(customPage, customPageDto);
            return customPageDto;
        }

        static partial void DoCustomMappings(CustomPage customPage, CustomPageDto customPageDto);

        public static CustomPageSimpleDto AsSimpleDto(this CustomPage customPage)
        {
            var customPageSimpleDto = new CustomPageSimpleDto()
            {
                CustomPageID = customPage.CustomPageID,
                CustomPageDisplayName = customPage.CustomPageDisplayName,
                CustomPageVanityUrl = customPage.CustomPageVanityUrl,
                CustomPageContent = customPage.CustomPageContent,
                MenuItemID = customPage.MenuItemID,
                SortOrder = customPage.SortOrder
            };
            DoCustomSimpleDtoMappings(customPage, customPageSimpleDto);
            return customPageSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomPage customPage, CustomPageSimpleDto customPageSimpleDto);
    }
}