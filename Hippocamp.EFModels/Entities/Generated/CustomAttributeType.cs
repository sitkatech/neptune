using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttributeType")]
    [Index(nameof(CustomAttributeTypeName), Name = "AK_CustomAttributeType_CustomAttributeTypeName", IsUnique = true)]
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
        public string CustomAttributeTypeName { get; set; }
        public int CustomAttributeDataTypeID { get; set; }
        public int? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        [StringLength(200)]
        public string CustomAttributeTypeDescription { get; set; }
        public int CustomAttributeTypePurposeID { get; set; }
        public string CustomAttributeTypeOptionsSchema { get; set; }

        [ForeignKey(nameof(CustomAttributeDataTypeID))]
        [InverseProperty("CustomAttributeTypes")]
        public virtual CustomAttributeDataType CustomAttributeDataType { get; set; }
        [ForeignKey(nameof(CustomAttributeTypePurposeID))]
        [InverseProperty("CustomAttributeTypes")]
        public virtual CustomAttributeTypePurpose CustomAttributeTypePurpose { get; set; }
        [ForeignKey(nameof(MeasurementUnitTypeID))]
        [InverseProperty("CustomAttributeTypes")]
        public virtual MeasurementUnitType MeasurementUnitType { get; set; }
        [InverseProperty(nameof(CustomAttribute.CustomAttributeType))]
        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        [InverseProperty(nameof(MaintenanceRecordObservation.CustomAttributeType))]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        [InverseProperty(nameof(TreatmentBMPTypeCustomAttributeType.CustomAttributeType))]
        public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
    }
}
