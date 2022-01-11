//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomPageRole]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class CustomPageRoleExtensionMethods
    {
        public static CustomPageRoleDto AsDto(this CustomPageRole customPageRole)
        {
            var customPageRoleDto = new CustomPageRoleDto()
            {
                CustomPageRoleID = customPageRole.CustomPageRoleID,
                CustomPage = customPageRole.CustomPage.AsDto(),
                Role = customPageRole.Role.AsDto()
            };
            DoCustomMappings(customPageRole, customPageRoleDto);
            return customPageRoleDto;
        }

        static partial void DoCustomMappings(CustomPageRole customPageRole, CustomPageRoleDto customPageRoleDto);

        public static CustomPageRoleSimpleDto AsSimpleDto(this CustomPageRole customPageRole)
        {
            var customPageRoleSimpleDto = new CustomPageRoleSimpleDto()
            {
                CustomPageRoleID = customPageRole.CustomPageRoleID,
                CustomPageID = customPageRole.CustomPageID,
                RoleID = customPageRole.RoleID
            };
            DoCustomSimpleDtoMappings(customPageRole, customPageRoleSimpleDto);
            return customPageRoleSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomPageRole customPageRole, CustomPageRoleSimpleDto customPageRoleSimpleDto);
    }
}