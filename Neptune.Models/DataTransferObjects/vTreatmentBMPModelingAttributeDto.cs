namespace Neptune.Models.DataTransferObjects;

public partial class vTreatmentBMPModelingAttributeDto
{
    public int TreatmentBMPID { get; set; }
    public int? UpstreamBMPID { get; set; }
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
    public string? RoutingConfiguration { get; set; }
    public double? StorageVolumeBelowLowestOutletElevation { get; set; }
    public double? SummerHarvestedWaterDemand { get; set; }
    public string? TimeOfConcentration { get; set; }
    public double? DrawdownTimeForDetentionVolume { get; set; }
    public double? TotalEffectiveBMPVolume { get; set; }
    public double? TotalEffectiveDrywellBMPVolume { get; set; }
    public double? TreatmentRate { get; set; }
    public string? UnderlyingHydrologicSoilGroup { get; set; }
    public double? UnderlyingInfiltrationRate { get; set; }
    public double? WaterQualityDetentionVolume { get; set; }
    public double? WettedFootprint { get; set; }
    public double? WinterHarvestedWaterDemand { get; set; }
    public string? MonthsOfOperation { get; set; }
    public string? DryWeatherFlowOverride { get; set; }
}