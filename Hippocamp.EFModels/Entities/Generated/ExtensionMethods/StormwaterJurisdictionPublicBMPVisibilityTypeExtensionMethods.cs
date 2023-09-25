//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPublicBMPVisibilityType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPublicBMPVisibilityTypeExtensionMethods
    {
        public static StormwaterJurisdictionPublicBMPVisibilityTypeDto AsDto(this StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType)
        {
            var stormwaterJurisdictionPublicBMPVisibilityTypeDto = new StormwaterJurisdictionPublicBMPVisibilityTypeDto()
            {
                StormwaterJurisdictionPublicBMPVisibilityTypeID = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeID,
                StormwaterJurisdictionPublicBMPVisibilityTypeName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeName,
                StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName
            };
            DoCustomMappings(stormwaterJurisdictionPublicBMPVisibilityType, stormwaterJurisdictionPublicBMPVisibilityTypeDto);
            return stormwaterJurisdictionPublicBMPVisibilityTypeDto;
        }

        static partial void DoCustomMappings(StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType, StormwaterJurisdictionPublicBMPVisibilityTypeDto stormwaterJurisdictionPublicBMPVisibilityTypeDto);

        public static StormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto AsSimpleDto(this StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType)
        {
            var stormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto = new StormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto()
            {
                StormwaterJurisdictionPublicBMPVisibilityTypeID = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeID,
                StormwaterJurisdictionPublicBMPVisibilityTypeName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeName,
                StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName
            };
            DoCustomSimpleDtoMappings(stormwaterJurisdictionPublicBMPVisibilityType, stormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto);
            return stormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType, StormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto stormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto);
    }
}