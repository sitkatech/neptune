//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PrecipitationZoneStagingExtensionMethods
    {
        public static PrecipitationZoneStagingDto AsDto(this PrecipitationZoneStaging precipitationZoneStaging)
        {
            var precipitationZoneStagingDto = new PrecipitationZoneStagingDto()
            {
                PrecipitationZoneStagingID = precipitationZoneStaging.PrecipitationZoneStagingID,
                PrecipitationZoneKey = precipitationZoneStaging.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZoneStaging.DesignStormwaterDepthInInches
            };
            DoCustomMappings(precipitationZoneStaging, precipitationZoneStagingDto);
            return precipitationZoneStagingDto;
        }

        static partial void DoCustomMappings(PrecipitationZoneStaging precipitationZoneStaging, PrecipitationZoneStagingDto precipitationZoneStagingDto);

        public static PrecipitationZoneStagingSimpleDto AsSimpleDto(this PrecipitationZoneStaging precipitationZoneStaging)
        {
            var precipitationZoneStagingSimpleDto = new PrecipitationZoneStagingSimpleDto()
            {
                PrecipitationZoneStagingID = precipitationZoneStaging.PrecipitationZoneStagingID,
                PrecipitationZoneKey = precipitationZoneStaging.PrecipitationZoneKey,
                DesignStormwaterDepthInInches = precipitationZoneStaging.DesignStormwaterDepthInInches
            };
            DoCustomSimpleDtoMappings(precipitationZoneStaging, precipitationZoneStagingSimpleDto);
            return precipitationZoneStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PrecipitationZoneStaging precipitationZoneStaging, PrecipitationZoneStagingSimpleDto precipitationZoneStagingSimpleDto);
    }
}