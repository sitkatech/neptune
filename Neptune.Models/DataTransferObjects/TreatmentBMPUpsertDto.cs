﻿using System;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPUpsertDto
    {
        public int TreatmentBMPID { get; set; }
        [Required]
        [Display(Name="Name")]
        public string? TreatmentBMPName { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int? TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }
        public string StormwaterJurisdictionName { get; set; }
        public string WatershedName { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }
        public string Notes { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DesignResidenceTimeforPermanentPool { get; set; }
        public double? DiversionRate { get; set; }
        public double? DrawdownTimeForWQDetentionVolume { get; set; }
        public double? EffectiveFootprint { get; set; }
        public double? EffectiveRetentionDepth { get; set; }
        public double? InfiltrationDischargeRate { get; set; }
        public double? InfiltrationSurfaceArea { get; set; }
        public double? MediaBedFootprint { get; set; }
        public double? PermanentPoolOrWetlandVolume { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public int? RoutingConfigurationID { get; set; }
        public int? TimeOfConcentrationID { get; set; }
        public int? UnderlyingHydrologicSoilGroupID { get; set; }
        public int? MonthsOfOperationID { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
        public bool? AreAllModelingAttributesComplete { get; set; }
        public bool? IsFullyParameterized { get; set; }

    }
}