using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanStatus")]
    [Index("WaterQualityManagementPlanStatusDisplayName", Name = "AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanStatusName", Name = "AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusName", IsUnique = true)]
    public partial class WaterQualityManagementPlanStatus
    {
        public WaterQualityManagementPlanStatus()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanStatusID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanStatusName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("WaterQualityManagementPlanStatus")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
