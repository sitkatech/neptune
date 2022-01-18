using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanPriority")]
    [Index(nameof(WaterQualityManagementPlanPriorityDisplayName), Name = "AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanPriorityName), Name = "AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityName", IsUnique = true)]
    public partial class WaterQualityManagementPlanPriority
    {
        public WaterQualityManagementPlanPriority()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanPriorityID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanPriorityName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanPriorityDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.WaterQualityManagementPlanPriority))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
