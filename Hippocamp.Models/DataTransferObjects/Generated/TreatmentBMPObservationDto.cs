//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }
    }

}