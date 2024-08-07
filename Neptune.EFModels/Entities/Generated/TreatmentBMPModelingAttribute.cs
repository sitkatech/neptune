﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPModelingAttribute")]
[Index("TreatmentBMPID", Name = "AK_TreatmentBMPModelingAttribute_TreatmentBMPID", IsUnique = true)]
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

    public double? DiversionRate { get; set; }

    public double? DrawdownTimeForWQDetentionVolume { get; set; }

    public double? EffectiveFootprint { get; set; }

    public double? EffectiveRetentionDepth { get; set; }

    public double? InfiltrationDischargeRate { get; set; }

    public double? InfiltrationSurfaceArea { get; set; }

    public double? MediaBedFootprint { get; set; }

    public double? PermanentPoolOrWetlandVolume { get; set; }

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

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("TreatmentBMPModelingAttributeTreatmentBMP")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [ForeignKey("UpstreamTreatmentBMPID")]
    [InverseProperty("TreatmentBMPModelingAttributeUpstreamTreatmentBMPs")]
    public virtual TreatmentBMP? UpstreamTreatmentBMP { get; set; }
}
