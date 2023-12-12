//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PermitType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PermitTypeExtensionMethods
    {
        public static PermitTypeSimpleDto AsSimpleDto(this PermitType permitType)
        {
            var dto = new PermitTypeSimpleDto()
            {
                PermitTypeID = permitType.PermitTypeID,
                PermitTypeName = permitType.PermitTypeName,
                PermitTypeDisplayName = permitType.PermitTypeDisplayName
            };
            return dto;
        }
    }
}