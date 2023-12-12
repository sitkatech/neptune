//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PrecipitationZoneStagingExtensionMethods
    {
        public static PrecipitationZoneStagingSimpleDto AsSimpleDto(this PrecipitationZoneStaging precipitationZoneStaging)
        {
            var dto = new PrecipitationZoneStagingSimpleDto()
            {
                PrecipitationZoneStagingID = precipitationZoneStaging.PrecipitationZoneStagingID,
                PrecipitationZoneKey = precipitationZoneStaging.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZoneStaging.DesignStormwaterDepthInInches
            };
            return dto;
        }
    }
}