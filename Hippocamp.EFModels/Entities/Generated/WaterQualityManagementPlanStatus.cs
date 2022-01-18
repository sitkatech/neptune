using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanStatus")]
    [Index(nameof(WaterQualityManagementPlanStatusDisplayName), Name = "AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanStatusName), Name = "AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusName", IsUnique = true)]
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
        public string WaterQualityManagementPlanStatusName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.WaterQualityManagementPlanStatus))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
