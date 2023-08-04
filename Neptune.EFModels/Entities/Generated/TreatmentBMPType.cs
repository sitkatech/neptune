using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPType")]
[Index("TreatmentBMPTypeName", Name = "AK_TreatmentBMPType_TreatmentBMPTypeName", IsUnique = true)]
public partial class TreatmentBMPType
{
    [Key]
    public int TreatmentBMPTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TreatmentBMPTypeName { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? TreatmentBMPTypeDescription { get; set; }

    public bool IsAnalyzedInModelingModule { get; set; }

    public int? TreatmentBMPModelingTypeID { get; set; }

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; } = new List<MaintenanceRecordObservation>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<QuickBMP> QuickBMPs { get; set; } = new List<QuickBMP>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; } = new List<TreatmentBMPAssessment>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; } = new List<TreatmentBMPBenchmarkAndThreshold>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; } = new List<TreatmentBMPObservation>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; } = new List<TreatmentBMPTypeAssessmentObservationType>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; } = new List<TreatmentBMPTypeCustomAttributeType>();

    [InverseProperty("TreatmentBMPType")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();
}
