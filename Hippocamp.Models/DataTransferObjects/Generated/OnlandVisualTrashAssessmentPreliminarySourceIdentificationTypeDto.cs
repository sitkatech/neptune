//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int PreliminarySourceIdentificationTypeID { get; set; }
        public string ExplanationIfTypeIsOther { get; set; }
    }

}