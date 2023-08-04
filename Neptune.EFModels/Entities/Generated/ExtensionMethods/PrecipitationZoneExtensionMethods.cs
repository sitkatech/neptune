//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PrecipitationZoneExtensionMethods
    {
        public static PrecipitationZoneDto AsDto(this PrecipitationZone precipitationZone)
        {
            var precipitationZoneDto = new PrecipitationZoneDto()
            {
                PrecipitationZoneID = precipitationZone.PrecipitationZoneID,
                PrecipitationZoneKey = precipitationZone.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZone.DesignStormwaterDepthInInches,
                LastUpdate = precipitationZone.LastUpdate
            };
            DoCustomMappings(precipitationZone, precipitationZoneDto);
            return precipitationZoneDto;
        }

        static partial void DoCustomMappings(PrecipitationZone precipitationZone, PrecipitationZoneDto precipitationZoneDto);

        public static PrecipitationZoneSimpleDto AsSimpleDto(this PrecipitationZone precipitationZone)
        {
            var precipitationZoneSimpleDto = new PrecipitationZoneSimpleDto()
            {
                PrecipitationZoneID = precipitationZone.PrecipitationZoneID,
                PrecipitationZoneKey = precipitationZone.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZone.DesignStormwaterDepthInInches,
                LastUpdate = precipitationZone.LastUpdate
            };
            DoCustomSimpleDtoMappings(precipitationZone, precipitationZoneSimpleDto);
            return precipitationZoneSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PrecipitationZone precipitationZone, PrecipitationZoneSimpleDto precipitationZoneSimpleDto);
    }
}