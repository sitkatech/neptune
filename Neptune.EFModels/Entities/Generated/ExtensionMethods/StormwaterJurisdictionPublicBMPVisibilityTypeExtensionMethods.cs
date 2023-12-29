//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPublicBMPVisibilityType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPublicBMPVisibilityTypeExtensionMethods
    {
        public static StormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto AsSimpleDto(this StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType)
        {
            var dto = new StormwaterJurisdictionPublicBMPVisibilityTypeSimpleDto()
            {
                StormwaterJurisdictionPublicBMPVisibilityTypeID = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeID,
                StormwaterJurisdictionPublicBMPVisibilityTypeName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeName,
                StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicBMPVisibilityType.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName
            };
            return dto;
        }
    }
}