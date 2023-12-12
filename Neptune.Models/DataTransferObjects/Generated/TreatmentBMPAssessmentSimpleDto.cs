//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]

namespace Neptune.Models.DataTransferObjects
{
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