//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PrecipitationZoneStagingDto
    {
        public int PrecipitationZoneStagingID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
    }

    public partial class PrecipitationZoneStagingSimpleDto
    {
        public int PrecipitationZoneStagingID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
    }

}