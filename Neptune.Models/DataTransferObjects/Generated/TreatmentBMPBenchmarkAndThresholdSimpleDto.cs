//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPBenchmarkAndThresholdSimpleDto
    {
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }
    }
}