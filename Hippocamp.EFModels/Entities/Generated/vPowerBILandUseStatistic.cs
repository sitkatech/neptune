using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPowerBILandUseStatistic
    {
        public int PrimaryKey { get; set; }
        public int HRUCharacteristicID { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public double Area { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string HRUCharacteristicLandUseCodeDisplayName { get; set; }
        public int? ModelBasinID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string WatershedName { get; set; }
        public int? CatchIDN { get; set; }
        public int? DownCatchIDN { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? LoadGeneratingUnitID { get; set; }
        public int? ModelBasinKey { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string LandUse { get; set; }
        [Required]
        [StringLength(132)]
        [Unicode(false)]
        public string SurfaceKey { get; set; }
    }
}
