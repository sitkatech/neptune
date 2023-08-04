using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("CustomAttribute")]
[Index("TreatmentBMPID", "TreatmentBMPTypeID", "CustomAttributeTypeID", Name = "AK_CustomAttribute_TreatmentBMPID_TreatmentBMPTypeID_CustomAttributeTypeID", IsUnique = true)]
public partial class CustomAttribute
{
    [Key]
    public int CustomAttributeID { get; set; }

    public int TreatmentBMPID { get; set; }

    public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    public int CustomAttributeTypeID { get; set; }

    [ForeignKey("CustomAttributeTypeID")]
    [InverseProperty("CustomAttributes")]
    public virtual CustomAttributeType CustomAttributeType { get; set; } = null!;

    [InverseProperty("CustomAttribute")]
    public virtual ICollection<CustomAttributeValue> CustomAttributeValues { get; set; } = new List<CustomAttributeValue>();

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("CustomAttributeTreatmentBMPs")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    public virtual TreatmentBMP TreatmentBMPNavigation { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("CustomAttributes")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("TreatmentBMPTypeCustomAttributeTypeID")]
    [InverseProperty("CustomAttributeTreatmentBMPTypeCustomAttributeTypes")]
    public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; } = null!;

    public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeTypeNavigation { get; set; } = null!;
}
