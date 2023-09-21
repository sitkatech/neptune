//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPublicWQMPVisibilityType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPublicWQMPVisibilityTypeExtensionMethods
    {
        public static StormwaterJurisdictionPublicWQMPVisibilityTypeDto AsDto(this StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType)
        {
            var stormwaterJurisdictionPublicWQMPVisibilityTypeDto = new StormwaterJurisdictionPublicWQMPVisibilityTypeDto()
            {
                StormwaterJurisdictionPublicWQMPVisibilityTypeID = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
                StormwaterJurisdictionPublicWQMPVisibilityTypeName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeName,
                StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName
            };
            DoCustomMappings(stormwaterJurisdictionPublicWQMPVisibilityType, stormwaterJurisdictionPublicWQMPVisibilityTypeDto);
            return stormwaterJurisdictionPublicWQMPVisibilityTypeDto;
        }

        static partial void DoCustomMappings(StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType, StormwaterJurisdictionPublicWQMPVisibilityTypeDto stormwaterJurisdictionPublicWQMPVisibilityTypeDto);

        public static StormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto AsSimpleDto(this StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType)
        {
            var stormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto = new StormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto()
            {
                StormwaterJurisdictionPublicWQMPVisibilityTypeID = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
                StormwaterJurisdictionPublicWQMPVisibilityTypeName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeName,
                StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName
            };
            DoCustomSimpleDtoMappings(stormwaterJurisdictionPublicWQMPVisibilityType, stormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto);
            return stormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType, StormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto stormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto);
    }
}