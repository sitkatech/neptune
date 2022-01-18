using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("MaintenanceRecordObservation")]
    public partial class MaintenanceRecordObservation
    {
        public MaintenanceRecordObservation()
        {
            MaintenanceRecordObservationValues = new HashSet<MaintenanceRecordObservationValue>();
        }

        [Key]
        public int MaintenanceRecordObservationID { get; set; }
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }

        [ForeignKey(nameof(CustomAttributeTypeID))]
        [InverseProperty("MaintenanceRecordObservations")]
        public virtual CustomAttributeType CustomAttributeType { get; set; }
        [ForeignKey(nameof(MaintenanceRecordID))]
        [InverseProperty("MaintenanceRecordObservationMaintenanceRecords")]
        public virtual MaintenanceRecord MaintenanceRecord { get; set; }
        public virtual MaintenanceRecord MaintenanceRecordNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeID))]
        [InverseProperty("MaintenanceRecordObservations")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeCustomAttributeTypeID))]
        [InverseProperty("MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypes")]
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; }
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeTypeNavigation { get; set; }
        [InverseProperty(nameof(MaintenanceRecordObservationValue.MaintenanceRecordObservation))]
        public virtual ICollection<MaintenanceRecordObservationValue> MaintenanceRecordObservationValues { get; set; }
    }
}
