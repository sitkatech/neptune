//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 FieldVisitID { get; set; }
        public System.Int32 TreatmentBMPAssessmentTypeID { get; set; }
        public string Notes { get; set; }
        public double? AssessmentScore { get; set; }
        public bool IsAssessmentComplete { get; set; }
    }

}