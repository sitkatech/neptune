using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPBenchmarkAndThreshold")]
    [Index(nameof(TreatmentBMPID), nameof(TreatmentBMPAssessmentObservationTypeID), Name = "AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_TreatmentBMPAssessmentObservationTypeID", IsUnique = true)]
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

        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholdTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMP1 { get; set; }
        [ForeignKey(nameof(TreatmentBMPAssessmentObservationTypeID))]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeID))]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeAssessmentObservationTypeID))]
        [InverseProperty("TreatmentBMPBenchmarkAndThresholdTreatmentBMPTypeAssessmentObservationTypes")]
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; }
    }
}
