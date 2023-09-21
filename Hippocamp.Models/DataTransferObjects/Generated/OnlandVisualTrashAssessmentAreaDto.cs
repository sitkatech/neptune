//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentAreaDto
    {
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public OnlandVisualTrashAssessmentScoreDto OnlandVisualTrashAssessmentBaselineScore { get; set; }
        public string AssessmentAreaDescription { get; set; }
        public OnlandVisualTrashAssessmentScoreDto OnlandVisualTrashAssessmentProgressScore { get; set; }
    }

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