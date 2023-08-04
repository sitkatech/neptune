//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPAssessmentObservationTypeDto
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }
        public ObservationTypeSpecificationDto ObservationTypeSpecification { get; set; }
        public string TreatmentBMPAssessmentObservationTypeSchema { get; set; }
    }

    public partial class TreatmentBMPAssessmentObservationTypeSimpleDto
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }
        public System.Int32 ObservationTypeSpecificationID { get; set; }
        public string TreatmentBMPAssessmentObservationTypeSchema { get; set; }
    }

}