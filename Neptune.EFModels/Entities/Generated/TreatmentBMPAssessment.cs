using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPAssessment")]
[Index("TreatmentBMPAssessmentID", "TreatmentBMPID", Name = "AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID", IsUnique = true)]
[Index("TreatmentBMPAssessmentID", "TreatmentBMPTypeID", Name = "AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID", IsUnique = true)]
public partial class TreatmentBMPAssessment
{
    [Key]
    public int TreatmentBMPAssessmentID { get; set; }

    public int TreatmentBMPID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int FieldVisitID { get; set; }

    public int TreatmentBMPAssessmentTypeID { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Notes { get; set; }

    public double? AssessmentScore { get; set; }

    public bool IsAssessmentComplete { get; set; }

    [ForeignKey("FieldVisitID")]
    [InverseProperty("TreatmentBMPAssessmentFieldVisits")]
    public virtual FieldVisit FieldVisit { get; set; } = null!;

    public virtual FieldVisit FieldVisitNavigation { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("TreatmentBMPAssessmentTreatmentBMPs")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [InverseProperty("TreatmentBMPAssessment")]
    public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; } = new List<TreatmentBMPAssessmentPhoto>();

    public virtual TreatmentBMP TreatmentBMPNavigation { get; set; } = null!;

    [InverseProperty("TreatmentBMPAssessment")]
    public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPAssessments { get; set; } = new List<TreatmentBMPObservation>();

    public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPs { get; set; } = new List<TreatmentBMPObservation>();

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("TreatmentBMPAssessments")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;
}
