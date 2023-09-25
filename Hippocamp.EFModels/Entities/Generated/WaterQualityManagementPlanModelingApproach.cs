using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("WaterQualityManagementPlanModelingApproach")]
    [Index("WaterQualityManagementPlanModelingApproachDisplayName", Name = "AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanModelingApproachName", Name = "AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachName", IsUnique = true)]
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
        [Unicode(false)]
        public string WaterQualityManagementPlanModelingApproachName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string WaterQualityManagementPlanModelingApproachDisplayName { get; set; }
        [Required]
        [StringLength(300)]
        [Unicode(false)]
        public string WaterQualityManagementPlanModelingApproachDescription { get; set; }

        [InverseProperty("WaterQualityManagementPlanModelingApproach")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
