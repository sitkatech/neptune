using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPAssessmentObservationType")]
[Index("TreatmentBMPAssessmentObservationTypeName", Name = "AK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeName", IsUnique = true)]
public partial class TreatmentBMPAssessmentObservationType
{
    [Key]
    public int TreatmentBMPAssessmentObservationTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TreatmentBMPAssessmentObservationTypeName { get; set; }

    public int ObservationTypeSpecificationID { get; set; }

    [Unicode(false)]
    public string? TreatmentBMPAssessmentObservationTypeSchema { get; set; }

    [InverseProperty("TreatmentBMPAssessmentObservationType")]
    public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; } = new List<TreatmentBMPBenchmarkAndThreshold>();

    [InverseProperty("TreatmentBMPAssessmentObservationType")]
    public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; } = new List<TreatmentBMPObservation>();

    [InverseProperty("TreatmentBMPAssessmentObservationType")]
    public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; } = new List<TreatmentBMPTypeAssessmentObservationType>();
}
