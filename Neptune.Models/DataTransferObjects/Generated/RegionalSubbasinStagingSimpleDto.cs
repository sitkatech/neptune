//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]

namespace Neptune.Models.DataTransferObjects
{
    public partial class RegionalSubbasinStagingSimpleDto
    {
        public int RegionalSubbasinStagingID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public int? OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
    }
}