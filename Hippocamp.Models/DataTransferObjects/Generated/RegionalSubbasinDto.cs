//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasin]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class RegionalSubbasinDto
    {
        public int RegionalSubbasinID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsWaitingForLGURefresh { get; set; }
        public bool? IsInModelBasin { get; set; }
        public ModelBasinDto ModelBasin { get; set; }
    }

    public partial class RegionalSubbasinSimpleDto
    {
        public int RegionalSubbasinID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsWaitingForLGURefresh { get; set; }
        public bool? IsInModelBasin { get; set; }
        public int? ModelBasinID { get; set; }
    }

}