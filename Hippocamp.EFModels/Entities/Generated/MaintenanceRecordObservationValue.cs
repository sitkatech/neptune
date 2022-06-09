﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("MaintenanceRecordObservationValue")]
    public partial class MaintenanceRecordObservationValue
    {
        [Key]
        public int MaintenanceRecordObservationValueID { get; set; }
        public int MaintenanceRecordObservationID { get; set; }
        [Required]
        [StringLength(1000)]
        [Unicode(false)]
        public string ObservationValue { get; set; }

        [ForeignKey("MaintenanceRecordObservationID")]
        [InverseProperty("MaintenanceRecordObservationValues")]
        public virtual MaintenanceRecordObservation MaintenanceRecordObservation { get; set; }
    }
}
