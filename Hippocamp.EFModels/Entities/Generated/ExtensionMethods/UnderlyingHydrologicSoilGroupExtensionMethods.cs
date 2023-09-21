//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[UnderlyingHydrologicSoilGroup]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class UnderlyingHydrologicSoilGroupExtensionMethods
    {
        public static UnderlyingHydrologicSoilGroupDto AsDto(this UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup)
        {
            var underlyingHydrologicSoilGroupDto = new UnderlyingHydrologicSoilGroupDto()
            {
                UnderlyingHydrologicSoilGroupID = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupID,
                UnderlyingHydrologicSoilGroupName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupName,
                UnderlyingHydrologicSoilGroupDisplayName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName
            };
            DoCustomMappings(underlyingHydrologicSoilGroup, underlyingHydrologicSoilGroupDto);
            return underlyingHydrologicSoilGroupDto;
        }

        static partial void DoCustomMappings(UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup, UnderlyingHydrologicSoilGroupDto underlyingHydrologicSoilGroupDto);

        public static UnderlyingHydrologicSoilGroupSimpleDto AsSimpleDto(this UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup)
        {
            var underlyingHydrologicSoilGroupSimpleDto = new UnderlyingHydrologicSoilGroupSimpleDto()
            {
                UnderlyingHydrologicSoilGroupID = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupID,
                UnderlyingHydrologicSoilGroupName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupName,
                UnderlyingHydrologicSoilGroupDisplayName = underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName
            };
            DoCustomSimpleDtoMappings(underlyingHydrologicSoilGroup, underlyingHydrologicSoilGroupSimpleDto);
            return underlyingHydrologicSoilGroupSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup, UnderlyingHydrologicSoilGroupSimpleDto underlyingHydrologicSoilGroupSimpleDto);
    }
}