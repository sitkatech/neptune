//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class PrecipitationZoneDto
    {
        public int PrecipitationZoneID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public partial class PrecipitationZoneSimpleDto
    {
        public int PrecipitationZoneID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
        public DateTime LastUpdate { get; set; }
    }

}