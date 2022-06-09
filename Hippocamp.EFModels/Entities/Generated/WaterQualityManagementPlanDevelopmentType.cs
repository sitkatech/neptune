using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanDevelopmentType")]
    [Index("WaterQualityManagementPlanDevelopmentTypeDisplayName", Name = "AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanDevelopmentTypeName", Name = "AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeName", IsUnique = true)]
    public partial class WaterQualityManagementPlanDevelopmentType
    {
        public WaterQualityManagementPlanDevelopmentType()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanDevelopmentTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("WaterQualityManagementPlanDevelopmentType")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
