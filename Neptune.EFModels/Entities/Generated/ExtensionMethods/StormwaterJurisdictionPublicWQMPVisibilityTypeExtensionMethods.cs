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
            var dto = new StormwaterJurisdictionPublicWQMPVisibilityTypeSimpleDto()
            {
                StormwaterJurisdictionPublicWQMPVisibilityTypeID = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
                StormwaterJurisdictionPublicWQMPVisibilityTypeName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeName,
                StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName = stormwaterJurisdictionPublicWQMPVisibilityType.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName
            };
            return dto;
        }
    }
}