//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]

namespace Neptune.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentAreaSimpleDto
    {
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentBaselineScoreID { get; set; }
        public string AssessmentAreaDescription { get; set; }
        public int? OnlandVisualTrashAssessmentProgressScoreID { get; set; }
    }
}