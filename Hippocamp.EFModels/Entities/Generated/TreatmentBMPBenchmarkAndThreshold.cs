using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPBenchmarkAndThreshold")]
    [Index("TreatmentBMPID", "TreatmentBMPAssessmentObservationTypeID", Name = "AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_TreatmentBMPAssessmentObservationTypeID", IsUnique = true)]
    public partial class TreatmentBMPBenchmarkAndThreshold
    {
        [Key]
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }

        [ForeignKey("TreatmentBMPID")]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholdTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMP1 { get; set; }
        [ForeignKey("TreatmentBMPAssessmentObservationTypeID")]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey("TreatmentBMPTypeAssessmentObservationTypeID")]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholdTreatmentBMPTypeAssessmentObservationTypes")]
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; }
    }
}
