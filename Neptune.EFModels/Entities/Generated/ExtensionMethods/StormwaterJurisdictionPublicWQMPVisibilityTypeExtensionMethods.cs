//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPublicWQMPVisibilityType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPublicWQMPVisibilityTypeExtensionMethods
    {

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