//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPObservationSimpleDto
    {
        public int TreatmentBMPObservationID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }
    }
}