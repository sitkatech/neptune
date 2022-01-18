using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vNereidLoadingInput
    {
        public int PrimaryKey { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int LSPCBasinKey { get; set; }
        [Required]
        [StringLength(100)]
        public string LandUseCode { get; set; }
        [Required]
        [StringLength(100)]
        public string BaselineLandUseCode { get; set; }
        [Required]
        [StringLength(5)]
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double Area { get; set; }
        public double ImperviousAcres { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public bool? DelineationIsVerified { get; set; }
        public int? SpatiallyAssociatedModelingApproach { get; set; }
        public int? RelationallyAssociatedModelingApproach { get; set; }
    }
}
