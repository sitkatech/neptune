using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifyTreatmentBMP")]
    [Index(nameof(TreatmentBMPID), nameof(WaterQualityManagementPlanVerifyTreatmentBMPID), Name = "AK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMPID_WaterQualityManagementPlanVerifyTreatmentBMPID", IsUnique = true)]
    public partial class WaterQualityManagementPlanVerifyTreatmentBMP
    {
        [Key]
        public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        [StringLength(500)]
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }

        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("WaterQualityManagementPlanVerifyTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyID))]
        [InverseProperty("WaterQualityManagementPlanVerifyTreatmentBMPs")]
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
    }
}
