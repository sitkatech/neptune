using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("MaintenanceRecordObservation")]
public partial class MaintenanceRecordObservation
{
    [Key]
    public int MaintenanceRecordObservationID { get; set; }

    public int MaintenanceRecordID { get; set; }

    public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int CustomAttributeTypeID { get; set; }

    [ForeignKey("CustomAttributeTypeID")]
    [InverseProperty("MaintenanceRecordObservations")]
    public virtual CustomAttributeType CustomAttributeType { get; set; } = null!;

    [ForeignKey("MaintenanceRecordID")]
    [InverseProperty("MaintenanceRecordObservationMaintenanceRecords")]
    public virtual MaintenanceRecord MaintenanceRecord { get; set; } = null!;

    public virtual MaintenanceRecord MaintenanceRecordNavigation { get; set; } = null!;

    [InverseProperty("MaintenanceRecordObservation")]
    public virtual ICollection<MaintenanceRecordObservationValue> MaintenanceRecordObservationValues { get; set; } = new List<MaintenanceRecordObservationValue>();

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("MaintenanceRecordObservations")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeCustomAttributeTypeID")]
    [InverseProperty("MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypes")]
    public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; } = null!;

    public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeTypeNavigation { get; set; } = null!;
}
