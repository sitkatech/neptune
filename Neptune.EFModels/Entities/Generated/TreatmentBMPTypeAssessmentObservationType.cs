using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPTypeAssessmentObservationType")]
[Index("TreatmentBMPTypeAssessmentObservationTypeID", "TreatmentBMPTypeID", "TreatmentBMPAssessmentObservationTypeID", Name = "AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_TreatmentBMPAssessme", IsUnique = true)]
[Index("TreatmentBMPTypeID", "TreatmentBMPAssessmentObservationTypeID", Name = "AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_TreatmentBMPAssessmentObservationTypeID", IsUnique = true)]
public partial class TreatmentBMPTypeAssessmentObservationType
{
    [Key]
    public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int TreatmentBMPAssessmentObservationTypeID { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? AssessmentScoreWeight { get; set; }

    public double? DefaultThresholdValue { get; set; }

    public double? DefaultBenchmarkValue { get; set; }

    public bool OverrideAssessmentScoreIfFailing { get; set; }

    public int? SortOrder { get; set; }

    [ForeignKey("TreatmentBMPAssessmentObservationTypeID")]
    [InverseProperty("TreatmentBMPTypeAssessmentObservationTypes")]
    public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; } = null!;

    [InverseProperty("TreatmentBMPTypeAssessmentObservationType")]
    public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; } = new List<TreatmentBMPBenchmarkAndThreshold>();

    [InverseProperty("TreatmentBMPTypeAssessmentObservationType")]
    public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; } = new List<TreatmentBMPObservation>();

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("TreatmentBMPTypeAssessmentObservationTypes")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;
}
