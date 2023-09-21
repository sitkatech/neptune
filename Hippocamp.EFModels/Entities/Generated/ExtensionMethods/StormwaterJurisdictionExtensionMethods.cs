//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class StormwaterJurisdictionExtensionMethods
    {
        public static StormwaterJurisdictionDto AsDto(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            var stormwaterJurisdictionDto = new StormwaterJurisdictionDto()
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                Organization = stormwaterJurisdiction.Organization.AsDto(),
                StateProvince = stormwaterJurisdiction.StateProvince.AsDto(),
                StormwaterJurisdictionPublicBMPVisibilityType = stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityType.AsDto(),
                StormwaterJurisdictionPublicWQMPVisibilityType = stormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityType.AsDto()
            };
            DoCustomMappings(stormwaterJurisdiction, stormwaterJurisdictionDto);
            return stormwaterJurisdictionDto;
        }

        static partial void DoCustomMappings(StormwaterJurisdiction stormwaterJurisdiction, StormwaterJurisdictionDto stormwaterJurisdictionDto);

        public static StormwaterJurisdictionSimpleDto AsSimpleDto(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            var stormwaterJurisdictionSimpleDto = new StormwaterJurisdictionSimpleDto()
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                OrganizationID = stormwaterJurisdiction.OrganizationID,
                StateProvinceID = stormwaterJurisdiction.StateProvinceID,
                StormwaterJurisdictionPublicBMPVisibilityTypeID = stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID,
                StormwaterJurisdictionPublicWQMPVisibilityTypeID = stormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID
            };
            DoCustomSimpleDtoMappings(stormwaterJurisdiction, stormwaterJurisdictionSimpleDto);
            return stormwaterJurisdictionSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdiction stormwaterJurisdiction, StormwaterJurisdictionSimpleDto stormwaterJurisdictionSimpleDto);
    }
}