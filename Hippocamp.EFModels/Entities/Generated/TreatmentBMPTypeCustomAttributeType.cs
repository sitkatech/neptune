using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPTypeCustomAttributeType")]
    [Index("TreatmentBMPTypeID", "CustomAttributeTypeID", Name = "AK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID", IsUnique = true)]
    public partial class TreatmentBMPTypeCustomAttributeType
    {
        public TreatmentBMPTypeCustomAttributeType()
        {
            CustomAttributeTreatmentBMPTypeCustomAttributeTypeNavigations = new HashSet<CustomAttribute>();
            CustomAttributeTreatmentBMPTypeCustomAttributeTypes = new HashSet<CustomAttribute>();
            MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypeNavigations = new HashSet<MaintenanceRecordObservation>();
            MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypes = new HashSet<MaintenanceRecordObservation>();
        }

        [Key]
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public int? SortOrder { get; set; }

        [ForeignKey("CustomAttributeTypeID")]
        [InverseProperty("TreatmentBMPTypeCustomAttributeTypes")]
        public virtual CustomAttributeType CustomAttributeType { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("TreatmentBMPTypeCustomAttributeTypes")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual ICollection<CustomAttribute> CustomAttributeTreatmentBMPTypeCustomAttributeTypeNavigations { get; set; }
        [InverseProperty("TreatmentBMPTypeCustomAttributeType")]
        public virtual ICollection<CustomAttribute> CustomAttributeTreatmentBMPTypeCustomAttributeTypes { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypeNavigations { get; set; }
        [InverseProperty("TreatmentBMPTypeCustomAttributeType")]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypes { get; set; }
    }
}
