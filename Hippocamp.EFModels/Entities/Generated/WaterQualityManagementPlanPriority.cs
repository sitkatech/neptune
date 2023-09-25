using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("WaterQualityManagementPlanPriority")]
    [Index("WaterQualityManagementPlanPriorityDisplayName", Name = "AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanPriorityName", Name = "AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityName", IsUnique = true)]
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
        [Unicode(false)]
        public string WaterQualityManagementPlanPriorityName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanPriorityDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("WaterQualityManagementPlanPriority")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
