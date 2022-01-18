using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("HydrologicSubarea")]
    [Index(nameof(HydrologicSubareaName), Name = "AK_HydrologicSubarea_HydrologicSubareaName", IsUnique = true)]
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
        public string HydrologicSubareaName { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlan.HydrologicSubarea))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
