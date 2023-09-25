using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("CustomAttributeType")]
    [Index("CustomAttributeTypeName", Name = "AK_CustomAttributeType_CustomAttributeTypeName", IsUnique = true)]
    public partial class CustomAttributeType
    {
        public CustomAttributeType()
        {
            CustomAttributes = new HashSet<CustomAttribute>();
            MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            TreatmentBMPTypeCustomAttributeTypes = new HashSet<TreatmentBMPTypeCustomAttributeType>();
        }

        [Key]
        public int CustomAttributeTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CustomAttributeTypeName { get; set; }
        public int CustomAttributeDataTypeID { get; set; }
        public int? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string CustomAttributeTypeDescription { get; set; }
        public int CustomAttributeTypePurposeID { get; set; }
        [Unicode(false)]
        public string CustomAttributeTypeOptionsSchema { get; set; }

        [ForeignKey("CustomAttributeDataTypeID")]
        [InverseProperty("CustomAttributeTypes")]
        public virtual CustomAttributeDataType CustomAttributeDataType { get; set; }
        [ForeignKey("CustomAttributeTypePurposeID")]
        [InverseProperty("CustomAttributeTypes")]
        public virtual CustomAttributeTypePurpose CustomAttributeTypePurpose { get; set; }
        [ForeignKey("MeasurementUnitTypeID")]
        [InverseProperty("CustomAttributeTypes")]
        public virtual MeasurementUnitType MeasurementUnitType { get; set; }
        [InverseProperty("CustomAttributeType")]
        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        [InverseProperty("CustomAttributeType")]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        [InverseProperty("CustomAttributeType")]
        public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
    }
}
