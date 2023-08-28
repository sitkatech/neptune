using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("MaintenanceRecord")]
[Index("FieldVisitID", Name = "AK_MaintenanceRecord_FieldVisitID", IsUnique = true)]
[Index("MaintenanceRecordID", "TreatmentBMPID", Name = "AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID", IsUnique = true)]
[Index("MaintenanceRecordID", "TreatmentBMPTypeID", Name = "AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID", IsUnique = true)]
public partial class MaintenanceRecord
{
    [Key]
    public int MaintenanceRecordID { get; set; }

    public int TreatmentBMPID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int FieldVisitID { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? MaintenanceRecordDescription { get; set; }

    public int? MaintenanceRecordTypeID { get; set; }

    [ForeignKey("FieldVisitID")]
    [InverseProperty("MaintenanceRecord")]
    public virtual FieldVisit FieldVisit { get; set; } = null!;

    [InverseProperty("MaintenanceRecord")]
    public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; } = new List<MaintenanceRecordObservation>();

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("MaintenanceRecords")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("MaintenanceRecords")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;
}
