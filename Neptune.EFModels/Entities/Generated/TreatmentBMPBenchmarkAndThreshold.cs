using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

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
    [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [ForeignKey("TreatmentBMPAssessmentObservationTypeID")]
    [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
    public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeAssessmentObservationTypeID")]
    [InverseProperty("TreatmentBMPBenchmarkAndThresholds")]
    public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; } = null!;
}
