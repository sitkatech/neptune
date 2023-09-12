﻿using Neptune.Models.DataTransferObjects;

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

}