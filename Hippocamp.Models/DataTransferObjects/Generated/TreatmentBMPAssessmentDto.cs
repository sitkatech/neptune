//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TreatmentBMPAssessmentDto
    {
        public int TreatmentBMPAssessmentID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public FieldVisitDto FieldVisit { get; set; }
        public TreatmentBMPAssessmentTypeDto TreatmentBMPAssessmentType { get; set; }
        public string Notes { get; set; }
        public double? AssessmentScore { get; set; }
        public bool IsAssessmentComplete { get; set; }
    }

    public partial class TreatmentBMPAssessmentSimpleDto
    {
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public int TreatmentBMPAssessmentTypeID { get; set; }
        public string Notes { get; set; }
        public double? AssessmentScore { get; set; }
        public bool IsAssessmentComplete { get; set; }
    }

}