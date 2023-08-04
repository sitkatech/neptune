//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentScore]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentScoreDto
    {
        public int OnlandVisualTrashAssessmentScoreID { get; set; }
        public string OnlandVisualTrashAssessmentScoreName { get; set; }
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int NumericValue { get; set; }
        public decimal TrashGenerationRate { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentScoreSimpleDto
    {
        public int OnlandVisualTrashAssessmentScoreID { get; set; }
        public string OnlandVisualTrashAssessmentScoreName { get; set; }
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int NumericValue { get; set; }
        public decimal TrashGenerationRate { get; set; }
    }

}