using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttribute")]
    [Index("TreatmentBMPID", "TreatmentBMPTypeID", "CustomAttributeTypeID", Name = "AK_CustomAttribute_TreatmentBMPID_TreatmentBMPTypeID_CustomAttributeTypeID", IsUnique = true)]
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

        [ForeignKey("CustomAttributeTypeID")]
        [InverseProperty("CustomAttributes")]
        public virtual CustomAttributeType CustomAttributeType { get; set; }
        [ForeignKey("TreatmentBMPID")]
        [InverseProperty("CustomAttributeTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("CustomAttributes")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey("TreatmentBMPTypeCustomAttributeTypeID")]
        [InverseProperty("CustomAttributeTreatmentBMPTypeCustomAttributeTypes")]
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; }
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeTypeNavigation { get; set; }
        [InverseProperty("CustomAttribute")]
        public virtual ICollection<CustomAttributeValue> CustomAttributeValues { get; set; }
    }
}
