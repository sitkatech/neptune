//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionExtensionMethods
    {

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