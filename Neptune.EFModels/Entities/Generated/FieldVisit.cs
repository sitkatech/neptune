using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("FieldVisit")]
public partial class FieldVisit
{
    [Key]
    public int FieldVisitID { get; set; }

    public int TreatmentBMPID { get; set; }

    public int FieldVisitStatusID { get; set; }

    public int PerformedByPersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime VisitDate { get; set; }

    public bool InventoryUpdated { get; set; }

    public int FieldVisitTypeID { get; set; }

    public bool IsFieldVisitVerified { get; set; }

    [InverseProperty("FieldVisit")]
    public virtual MaintenanceRecord? MaintenanceRecord { get; set; }

    [ForeignKey("PerformedByPersonID")]
    [InverseProperty("FieldVisits")]
    public virtual Person PerformedByPerson { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("FieldVisit")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [InverseProperty("FieldVisit")]
    public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; } = new List<TreatmentBMPAssessment>();
}
