//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPObservationDto
    {
        public int TreatmentBMPObservationID { get; set; }
        public TreatmentBMPAssessmentDto TreatmentBMPAssessment { get; set; }
        public TreatmentBMPTypeAssessmentObservationTypeDto TreatmentBMPTypeAssessmentObservationType { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public TreatmentBMPAssessmentObservationTypeDto TreatmentBMPAssessmentObservationType { get; set; }
        public string ObservationData { get; set; }
    }

    public partial class TreatmentBMPObservationSimpleDto
    {
        public int TreatmentBMPObservationID { get; set; }
        public System.Int32 TreatmentBMPAssessmentID { get; set; }
        public System.Int32 TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }
    }

}