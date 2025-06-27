namespace Neptune.EFModels.Entities;

public interface IvTreatmentBMPModelingAttribute
{
    double? AverageDivertedFlowrate { get; set; }
    double? AverageTreatmentFlowrate { get; set; }
    double? DesignDryWeatherTreatmentCapacity { get; set; }
    double? DesignLowFlowDiversionCapacity { get; set; }
    double? DesignMediaFiltrationRate { get; set; }
    double? DiversionRate { get; set; }
    double? DrawdownTimeForWQDetentionVolume { get; set; }
    double? EffectiveFootprint { get; set; }
    double? EffectiveRetentionDepth { get; set; }
    double? InfiltrationDischargeRate { get; set; }
    double? InfiltrationSurfaceArea { get; set; }
    double? MediaBedFootprint { get; set; }
    double? PermanentPoolOrWetlandVolume { get; set; }
    string? RoutingConfiguration { get; set; }
    double? StorageVolumeBelowLowestOutletElevation { get; set; }
    double? SummerHarvestedWaterDemand { get; set; }
    string? TimeOfConcentration { get; set; }
    double? DrawdownTimeForDetentionVolume { get; set; }
    double? TotalEffectiveBMPVolume { get; set; }
    double? TotalEffectiveDrywellBMPVolume { get; set; }
    double? TreatmentRate { get; set; }
    string? UnderlyingHydrologicSoilGroup { get; set; }
    double? UnderlyingInfiltrationRate { get; set; }
    double? WaterQualityDetentionVolume { get; set; }
    double? WettedFootprint { get; set; }
    double? WinterHarvestedWaterDemand { get; set; }
    string? MonthsOfOperation { get; set; }
    string? DryWeatherFlowOverride { get; set; }
}