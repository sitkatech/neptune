using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("FieldVisit")]
[Index("FieldVisitID", "TreatmentBMPID", Name = "AK_FieldVisit_FieldVisitID_TreatmentBMPID", IsUnique = true)]
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
    public virtual MaintenanceRecord? MaintenanceRecordFieldVisit { get; set; }

    public virtual ICollection<MaintenanceRecord> MaintenanceRecordFieldVisitNavigations { get; set; } = new List<MaintenanceRecord>();

    [ForeignKey("PerformedByPersonID")]
    [InverseProperty("FieldVisits")]
    public virtual Person PerformedByPerson { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("FieldVisit")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentFieldVisitNavigations { get; set; } = new List<TreatmentBMPAssessment>();

    [InverseProperty("FieldVisit")]
    public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentFieldVisits { get; set; } = new List<TreatmentBMPAssessment>();
}
