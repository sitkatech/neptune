using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPowerBITreatmentBMP
    {
        public int PrimaryKey { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TreatmentBMPTypeName { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string Jurisdiction { get; set; }
        public double? LocationLon { get; set; }
        public double? LocationLat { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Watershed { get; set; }
        public int? UpstreamBMPID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DelineationType { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? TreatmentBMPModelingAttributeID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? UpstreamTreatmentBMPID { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DiversionRate { get; set; }
        public double? DrawdownTimeforWQDetentionVolume { get; set; }
        public double? EffectiveFootprint { get; set; }
        public double? EffectiveRetentionDepth { get; set; }
        public double? InfiltrationDischargeRate { get; set; }
        public double? InfiltrationSurfaceArea { get; set; }
        public double? MediaBedFootprint { get; set; }
        public double? PermanentPoolorWetlandVolume { get; set; }
        public int? RoutingConfigurationID { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public int? TimeOfConcentrationID { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public int? UnderlyingHydrologicSoilGroupID { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public int? MonthsOfOperationID { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
    }
}
