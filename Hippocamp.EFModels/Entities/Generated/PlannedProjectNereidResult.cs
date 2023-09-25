﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Neptune.EFModels.Entities
{
    [Table("PlannedProjectNereidResult")]
    public partial class PlannedProjectNereidResult
    {
        [Key]
        public int PlannedProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("PlannedProjectNereidResults")]
        public virtual Project Project { get; set; }
    }
}
