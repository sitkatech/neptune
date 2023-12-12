//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Role]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RoleExtensionMethods
    {
        public static RoleSimpleDto AsSimpleDto(this Role role)
        {
            var dto = new RoleSimpleDto()
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName,
                RoleDisplayName = role.RoleDisplayName,
                RoleDescription = role.RoleDescription
            };
            return dto;
        }
    }
}