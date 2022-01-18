using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPowerBIWaterQualityManagementPlan
    {
        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanName { get; set; }
        [Required]
        [StringLength(200)]
        public string OrganizationName { get; set; }
        [StringLength(100)]
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanLandUseDisplayName { get; set; }
        [StringLength(100)]
        public string WaterQualityManagementPlanPermitTermDisplayName { get; set; }
        public int? ApprovalDate { get; set; }
        public int? DateOfConstruction { get; set; }
        [StringLength(100)]
        public string HydromodificationAppliesDisplayName { get; set; }
        [StringLength(100)]
        public string HydrologicSubareaName { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        [Required]
        [StringLength(50)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        [StringLength(50)]
        public string ModelingApproach { get; set; }
    }
}
