using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("QuickBMP")]
    [Index("WaterQualityManagementPlanID", "QuickBMPName", Name = "AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName", IsUnique = true)]
    public partial class QuickBMP
    {
        public QuickBMP()
        {
            WaterQualityManagementPlanVerifyQuickBMPs = new HashSet<WaterQualityManagementPlanVerifyQuickBMP>();
        }

        [Key]
        public int QuickBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string QuickBMPName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string QuickBMPNote { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PercentOfSiteTreated { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PercentCaptured { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PercentRetained { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }

        [ForeignKey("DryWeatherFlowOverrideID")]
        [InverseProperty("QuickBMPs")]
        public virtual DryWeatherFlowOverride DryWeatherFlowOverride { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("QuickBMPs")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("QuickBMPs")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty("QuickBMP")]
        public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
    }
}
