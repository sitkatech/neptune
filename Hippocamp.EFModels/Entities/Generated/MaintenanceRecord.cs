﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("MaintenanceRecord")]
    [Index("FieldVisitID", Name = "AK_MaintenanceRecord_FieldVisitID", IsUnique = true)]
    [Index("MaintenanceRecordID", "TreatmentBMPID", Name = "AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID", IsUnique = true)]
    [Index("MaintenanceRecordID", "TreatmentBMPTypeID", Name = "AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID", IsUnique = true)]
    public partial class MaintenanceRecord
    {
        public MaintenanceRecord()
        {
            MaintenanceRecordObservationMaintenanceRecordNavigations = new HashSet<MaintenanceRecordObservation>();
            MaintenanceRecordObservationMaintenanceRecords = new HashSet<MaintenanceRecordObservation>();
        }

        [Key]
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string MaintenanceRecordDescription { get; set; }
        public int? MaintenanceRecordTypeID { get; set; }

        [ForeignKey("FieldVisitID")]
        [InverseProperty("MaintenanceRecordFieldVisit")]
        public virtual FieldVisit FieldVisit { get; set; }
        public virtual FieldVisit FieldVisitNavigation { get; set; }
        [ForeignKey("MaintenanceRecordTypeID")]
        [InverseProperty("MaintenanceRecords")]
        public virtual MaintenanceRecordType MaintenanceRecordType { get; set; }
        [ForeignKey("TreatmentBMPID")]
        [InverseProperty("MaintenanceRecordTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("MaintenanceRecords")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservationMaintenanceRecordNavigations { get; set; }
        [InverseProperty("MaintenanceRecord")]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservationMaintenanceRecords { get; set; }
    }
}