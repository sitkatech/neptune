//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPBenchmarkAndThresholdDto
    {
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public TreatmentBMPTypeAssessmentObservationTypeDto TreatmentBMPTypeAssessmentObservationType { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public TreatmentBMPAssessmentObservationTypeDto TreatmentBMPAssessmentObservationType { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }
    }

    public partial class TreatmentBMPBenchmarkAndThresholdSimpleDto
    {
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 TreatmentBMPAssessmentObservationTypeID { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }
    }

}