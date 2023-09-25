//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PermitType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PermitTypeExtensionMethods
    {
        public static PermitTypeDto AsDto(this PermitType permitType)
        {
            var permitTypeDto = new PermitTypeDto()
            {
                PermitTypeID = permitType.PermitTypeID,
                PermitTypeName = permitType.PermitTypeName,
                PermitTypeDisplayName = permitType.PermitTypeDisplayName
            };
            DoCustomMappings(permitType, permitTypeDto);
            return permitTypeDto;
        }

        static partial void DoCustomMappings(PermitType permitType, PermitTypeDto permitTypeDto);

        public static PermitTypeSimpleDto AsSimpleDto(this PermitType permitType)
        {
            var permitTypeSimpleDto = new PermitTypeSimpleDto()
            {
                PermitTypeID = permitType.PermitTypeID,
                PermitTypeName = permitType.PermitTypeName,
                PermitTypeDisplayName = permitType.PermitTypeDisplayName
            };
            DoCustomSimpleDtoMappings(permitType, permitTypeSimpleDto);
            return permitTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PermitType permitType, PermitTypeSimpleDto permitTypeSimpleDto);
    }
}