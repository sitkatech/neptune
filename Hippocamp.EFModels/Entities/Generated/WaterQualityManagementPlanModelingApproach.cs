using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanModelingApproach")]
    [Index(nameof(WaterQualityManagementPlanModelingApproachDisplayName), Name = "AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanModelingApproachName), Name = "AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachName", IsUnique = true)]
    public partial class WaterQualityManagementPlanModelingApproach
    {
        public WaterQualityManagementPlanModelingApproach()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanModelingApproachID { get; set; }
        [Required]
        [StringLength(50)]
        public string WaterQualityManagementPlanModelingApproachName { get; set; }
        [Required]
        [StringLength(50)]
        public string WaterQualityManagementPlanModelingApproachDisplayName { get; set; }
        [Required]
        [StringLength(300)]
        public string WaterQualityManagementPlanModelingApproachDescription { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproach))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
