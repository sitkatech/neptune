﻿using System;
using NetTopologySuite.Geometries;

namespace Hippocamp.Models.DataTransferObjects
{
    public class TreatmentBMPUpsertDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string? TreatmentBMPTypeName { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }
        public string? StormwaterJurisdictionName { get; set; }
        public string? WatershedName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Notes { get; set; }
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
        public RoutingConfigurationDto? RoutingConfiguration { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public TimeOfConcentrationDto? TimeOfConcentration { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public UnderlyingHydrologicSoilGroupDto? UnderlyingHydrologicSoilGroup { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public MonthsOfOperationDto? MonthsOfOperation { get; set; }
        public DryWeatherFlowOverrideDto? DryWeatherFlowOverride { get; set; }

    }
}