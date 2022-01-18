using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifyQuickBMP")]
    [Index(nameof(QuickBMPID), nameof(WaterQualityManagementPlanVerifyQuickBMPID), Name = "AK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMPID_WaterQualityManagementPlanVerifyQuickBMPID", IsUnique = true)]
    public partial class WaterQualityManagementPlanVerifyQuickBMP
    {
        [Key]
        public int WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        [StringLength(500)]
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }

        [ForeignKey(nameof(QuickBMPID))]
        [InverseProperty("WaterQualityManagementPlanVerifyQuickBMPs")]
        public virtual QuickBMP QuickBMP { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyID))]
        [InverseProperty("WaterQualityManagementPlanVerifyQuickBMPs")]
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
    }
}
