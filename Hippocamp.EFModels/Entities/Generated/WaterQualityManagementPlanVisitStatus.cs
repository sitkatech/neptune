using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVisitStatus")]
    public partial class WaterQualityManagementPlanVisitStatus
    {
        public WaterQualityManagementPlanVisitStatus()
        {
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanVisitStatusName { get; set; }

        [InverseProperty("WaterQualityManagementPlanVisitStatus")]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
