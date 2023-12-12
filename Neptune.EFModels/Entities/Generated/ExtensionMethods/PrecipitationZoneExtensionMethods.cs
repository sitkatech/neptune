//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PrecipitationZoneExtensionMethods
    {
        public static PrecipitationZoneSimpleDto AsSimpleDto(this PrecipitationZone precipitationZone)
        {
            var dto = new PrecipitationZoneSimpleDto()
            {
                PrecipitationZoneID = precipitationZone.PrecipitationZoneID,
                PrecipitationZoneKey = precipitationZone.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZone.DesignStormwaterDepthInInches,
                LastUpdate = precipitationZone.LastUpdate
            };
            return dto;
        }
    }
}