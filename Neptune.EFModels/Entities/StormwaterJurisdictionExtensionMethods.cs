using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class StormwaterJurisdictionExtensionMethods
{
    public static StormwaterJurisdictionDisplayDto AsDisplayDto(this StormwaterJurisdiction stormwaterJurisdiction)
    {
        var treatmentBMPSimpleDto = new StormwaterJurisdictionDisplayDto()
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
            StormwaterJurisdictionDisplayName = stormwaterJurisdiction.Organization.OrganizationName
        };
        return treatmentBMPSimpleDto;
    }

    public static StormwaterJurisdictionDto AsDto(this StormwaterJurisdiction stormwaterJurisdiction)
    {
        var dto = new StormwaterJurisdictionDto()
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
            OrganizationID = stormwaterJurisdiction.OrganizationID,
            StateProvinceID = stormwaterJurisdiction.StateProvinceID,
            StormwaterJurisdictionPublicBMPVisibilityTypeID = stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID,
            StormwaterJurisdictionPublicWQMPVisibilityTypeID = stormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
            Organization = stormwaterJurisdiction.Organization.AsSimpleDto()
        };
        return dto;
    }
}