using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPModelingAttribute")]
    [Index(nameof(TreatmentBMPID), Name = "AK_TreatmentBMPModelingAttribute_TreatmentBMPID", IsUnique = true)]
    public partial class TreatmentBMPModelingAttribute
    {
        [Key]
        public int TreatmentBMPModelingAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamTreatmentBMPID { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DesignResidenceTimeforPermanentPool { get; set; }
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

        [ForeignKey(nameof(DryWeatherFlowOverrideID))]
        [InverseProperty("TreatmentBMPModelingAttributes")]
        public virtual DryWeatherFlowOverride DryWeatherFlowOverride { get; set; }
        [ForeignKey(nameof(MonthsOfOperationID))]
        [InverseProperty("TreatmentBMPModelingAttributes")]
        public virtual MonthsOfOperation MonthsOfOperation { get; set; }
        [ForeignKey(nameof(RoutingConfigurationID))]
        [InverseProperty("TreatmentBMPModelingAttributes")]
        public virtual RoutingConfiguration RoutingConfiguration { get; set; }
        [ForeignKey(nameof(TimeOfConcentrationID))]
        [InverseProperty("TreatmentBMPModelingAttributes")]
        public virtual TimeOfConcentration TimeOfConcentration { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("TreatmentBMPModelingAttributeTreatmentBMP")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey(nameof(UnderlyingHydrologicSoilGroupID))]
        [InverseProperty("TreatmentBMPModelingAttributes")]
        public virtual UnderlyingHydrologicSoilGroup UnderlyingHydrologicSoilGroup { get; set; }
        [ForeignKey(nameof(UpstreamTreatmentBMPID))]
        [InverseProperty("TreatmentBMPModelingAttributeUpstreamTreatmentBMPs")]
        public virtual TreatmentBMP UpstreamTreatmentBMP { get; set; }
    }
}
