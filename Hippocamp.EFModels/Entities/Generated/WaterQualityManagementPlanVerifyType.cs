using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifyType")]
    public partial class WaterQualityManagementPlanVerifyType
    {
        public WaterQualityManagementPlanVerifyType()
        {
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanVerifyTypeName { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyType))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
