using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("CustomAttributeType")]
[Index("CustomAttributeTypeName", Name = "AK_CustomAttributeType_CustomAttributeTypeName", IsUnique = true)]
public partial class CustomAttributeType
{
    [Key]
    public int CustomAttributeTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CustomAttributeTypeName { get; set; }

    public int CustomAttributeDataTypeID { get; set; }

    public int? MeasurementUnitTypeID { get; set; }

    public bool IsRequired { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CustomAttributeTypeDescription { get; set; }

    public int CustomAttributeTypePurposeID { get; set; }

    [Unicode(false)]
    public string? CustomAttributeTypeOptionsSchema { get; set; }

    [InverseProperty("CustomAttributeType")]
    public virtual ICollection<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();

    [InverseProperty("CustomAttributeType")]
    public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; } = new List<MaintenanceRecordObservation>();

    [InverseProperty("CustomAttributeType")]
    public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; } = new List<TreatmentBMPTypeCustomAttributeType>();
}
