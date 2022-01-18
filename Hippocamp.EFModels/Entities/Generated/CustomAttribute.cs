using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttribute")]
    [Index(nameof(TreatmentBMPID), nameof(TreatmentBMPTypeID), nameof(CustomAttributeTypeID), Name = "AK_CustomAttribute_TreatmentBMPID_TreatmentBMPTypeID_CustomAttributeTypeID", IsUnique = true)]
    public partial class CustomAttribute
    {
        public CustomAttribute()
        {
            CustomAttributeValues = new HashSet<CustomAttributeValue>();
        }

        [Key]
        public int CustomAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }

        [ForeignKey(nameof(CustomAttributeTypeID))]
        [InverseProperty("CustomAttributes")]
        public virtual CustomAttributeType CustomAttributeType { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("CustomAttributeTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeID))]
        [InverseProperty("CustomAttributes")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeCustomAttributeTypeID))]
        [InverseProperty("CustomAttributeTreatmentBMPTypeCustomAttributeTypes")]
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; }
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeTypeNavigation { get; set; }
        [InverseProperty(nameof(CustomAttributeValue.CustomAttribute))]
        public virtual ICollection<CustomAttributeValue> CustomAttributeValues { get; set; }
    }
}
