//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TreatmentBMPTypeAssessmentObservationTypeDto
    {
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public TreatmentBMPAssessmentObservationTypeDto TreatmentBMPAssessmentObservationType { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool OverrideAssessmentScoreIfFailing { get; set; }
        public int? SortOrder { get; set; }
    }

    public partial class TreatmentBMPTypeAssessmentObservationTypeSimpleDto
    {
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool OverrideAssessmentScoreIfFailing { get; set; }
        public int? SortOrder { get; set; }
    }

}