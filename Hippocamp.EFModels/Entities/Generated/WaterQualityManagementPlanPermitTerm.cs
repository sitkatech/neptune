using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("WaterQualityManagementPlanPermitTerm")]
    [Index("WaterQualityManagementPlanPermitTermDisplayName", Name = "AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanPermitTermName", Name = "AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermName", IsUnique = true)]
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
        [Unicode(false)]
        public string WaterQualityManagementPlanPermitTermName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanPermitTermDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("WaterQualityManagementPlanPermitTerm")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
