using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("HydrologicSubarea")]
    [Index("HydrologicSubareaName", Name = "AK_HydrologicSubarea_HydrologicSubareaName", IsUnique = true)]
    public partial class HydrologicSubarea
    {
        public HydrologicSubarea()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int HydrologicSubareaID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string HydrologicSubareaName { get; set; }

        [InverseProperty("HydrologicSubarea")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
