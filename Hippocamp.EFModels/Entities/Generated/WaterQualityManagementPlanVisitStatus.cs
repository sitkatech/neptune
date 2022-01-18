using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

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
        public string WaterQualityManagementPlanVisitStatusName { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlanVerify.WaterQualityManagementPlanVisitStatus))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
