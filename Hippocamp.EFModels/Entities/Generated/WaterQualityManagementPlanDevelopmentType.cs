using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanDevelopmentType")]
    [Index(nameof(WaterQualityManagementPlanDevelopmentTypeDisplayName), Name = "AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanDevelopmentTypeName), Name = "AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeName", IsUnique = true)]
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
        public string WaterQualityManagementPlanDevelopmentTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.WaterQualityManagementPlanDevelopmentType))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
