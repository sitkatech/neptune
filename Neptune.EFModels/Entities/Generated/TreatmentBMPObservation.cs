using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPObservation")]
public partial class TreatmentBMPObservation
{
    [Key]
    public int TreatmentBMPObservationID { get; set; }

    public int TreatmentBMPAssessmentID { get; set; }

    public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int TreatmentBMPAssessmentObservationTypeID { get; set; }

    public string? ObservationData { get; set; }

    public virtual TreatmentBMPAssessment TreatmentBMP { get; set; } = null!;

    [ForeignKey("TreatmentBMPAssessmentID")]
    [InverseProperty("TreatmentBMPObservationTreatmentBMPAssessments")]
    public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; } = null!;

    [ForeignKey("TreatmentBMPAssessmentObservationTypeID")]
    [InverseProperty("TreatmentBMPObservations")]
    public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; } = null!;

    public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPNavigation { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("TreatmentBMPObservations")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeAssessmentObservationTypeID")]
    [InverseProperty("TreatmentBMPObservationTreatmentBMPTypeAssessmentObservationTypes")]
    public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; } = null!;
}
