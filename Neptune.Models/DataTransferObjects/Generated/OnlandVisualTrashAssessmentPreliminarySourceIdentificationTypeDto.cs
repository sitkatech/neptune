//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto
    {
        public int OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID { get; set; }
        public OnlandVisualTrashAssessmentDto OnlandVisualTrashAssessment { get; set; }
        public PreliminarySourceIdentificationTypeDto PreliminarySourceIdentificationType { get; set; }
        public string ExplanationIfTypeIsOther { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto
    {
        public int OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID { get; set; }
        public System.Int32 OnlandVisualTrashAssessmentID { get; set; }
        public System.Int32 PreliminarySourceIdentificationTypeID { get; set; }
        public string ExplanationIfTypeIsOther { get; set; }
    }

}