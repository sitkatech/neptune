using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("WaterQualityManagementPlanLandUse")]
    [Index("WaterQualityManagementPlanLandUseDisplayName", Name = "AK_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanLandUseName", Name = "AK_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseName", IsUnique = true)]
    public partial class WaterQualityManagementPlanLandUse
    {
        public WaterQualityManagementPlanLandUse()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanLandUseID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanLandUseName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanLandUseDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("WaterQualityManagementPlanLandUse")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
