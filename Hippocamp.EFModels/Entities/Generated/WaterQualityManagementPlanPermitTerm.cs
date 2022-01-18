using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanPermitTerm")]
    [Index(nameof(WaterQualityManagementPlanPermitTermDisplayName), Name = "AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanPermitTermName), Name = "AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermName", IsUnique = true)]
    public partial class WaterQualityManagementPlanPermitTerm
    {
        public WaterQualityManagementPlanPermitTerm()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int WaterQualityManagementPlanPermitTermID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanPermitTermName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanPermitTermDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.WaterQualityManagementPlanPermitTerm))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
