//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[UnderlyingHydrologicSoilGroup]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class UnderlyingHydrologicSoilGroupExtensionMethods
    {
        public static UnderlyingHydrologicSoilGroupSimpleDto AsSimpleDto(this UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup)
        {
            var dto = new UnderlyingHydrologicSoilGroupSimpleDto()
            {
                UnderlyingHydrologicSoilGroupID = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupID,
                UnderlyingHydrologicSoilGroupName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupName,
                UnderlyingHydrologicSoilGroupDisplayName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName
            };
            return dto;
        }
    }
}