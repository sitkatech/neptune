using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifyStatus")]
    public partial class WaterQualityManagementPlanVerifyStatus
    {
        public WaterQualityManagementPlanVerifyStatus()
        {
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int WaterQualityManagementPlanVerifyStatusID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanVerifyStatusName { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatus))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
