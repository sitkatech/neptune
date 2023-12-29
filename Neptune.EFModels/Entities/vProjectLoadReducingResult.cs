namespace Neptune.EFModels.Entities;

public partial class vProjectLoadReducingResult
{
    public double DryWeatherInflow => SummerDryWeatherInflow ?? 0 + WinterDryWeatherInflow ?? 0;
    public double DryWeatherTreated => SummerDryWeatherTreated ?? 0 + WinterDryWeatherTreated ?? 0;
    public double DryWeatherRetained => SummerDryWeatherRetained ?? 0 + WinterDryWeatherRetained ?? 0;
    public double DryWeatherUntreated => SummerDryWeatherUntreated ?? 0 + WinterDryWeatherUntreated ?? 0;
    public double DryWeatherTSSReduced => SummerDryWeatherTSSReduced ?? 0 + WinterDryWeatherTSSReduced ?? 0;
    public double DryWeatherTNReduced => SummerDryWeatherTNReduced ?? 0 + WinterDryWeatherTNReduced ?? 0;
    public double DryWeatherTPReduced => SummerDryWeatherTPReduced ?? 0 + WinterDryWeatherTPReduced ?? 0;
    public double DryWeatherFCReduced => SummerDryWeatherFCReduced ?? 0 + WinterDryWeatherFCReduced ?? 0;
    public double DryWeatherTCuReduced => SummerDryWeatherTCuReduced ?? 0 + WinterDryWeatherTCuReduced ?? 0;
    public double DryWeatherTPbReduced => SummerDryWeatherTPbReduced ?? 0 + WinterDryWeatherTPbReduced ?? 0;
    public double DryWeatherTZnReduced => SummerDryWeatherTZnReduced ?? 0 + WinterDryWeatherTZnReduced ?? 0;
    public double DryWeatherTSSInflow => SummerDryWeatherTSSInflow ?? 0 + WinterDryWeatherTSSInflow ?? 0;
    public double DryWeatherTNInflow => SummerDryWeatherTNInflow ?? 0 + WinterDryWeatherTNInflow ?? 0;
    public double DryWeatherTPInflow => SummerDryWeatherTPInflow ?? 0 + WinterDryWeatherTPInflow ?? 0;
    public double DryWeatherFCInflow => SummerDryWeatherFCInflow ?? 0 + WinterDryWeatherFCInflow ?? 0;
    public double DryWeatherTCuInflow => SummerDryWeatherTCuInflow ?? 0 + WinterDryWeatherTCuInflow ?? 0;
    public double DryWeatherTPbInflow => SummerDryWeatherTPbInflow ?? 0 + WinterDryWeatherTPbInflow ?? 0;
    public double DryWeatherTZnInflow => SummerDryWeatherTZnInflow ?? 0 + WinterDryWeatherTZnInflow ?? 0;
    public double TotalInflow => DryWeatherInflow + WetWeatherInflow ?? 0;
    public double TotalTreated => DryWeatherTreated + WetWeatherTreated ?? 0;
    public double TotalRetained => DryWeatherRetained + WetWeatherRetained ?? 0;
    public double TotalUntreated => DryWeatherUntreated + WetWeatherUntreated ?? 0;
    public double TotalTSSReduced => DryWeatherTSSReduced + WetWeatherTSSReduced ?? 0;
    public double TotalTNReduced => DryWeatherTNReduced + WetWeatherTNReduced ?? 0;
    public double TotalTPReduced => DryWeatherTPReduced + WetWeatherTPReduced ?? 0;
    public double TotalFCReduced => DryWeatherFCReduced + WetWeatherFCReduced ?? 0;
    public double TotalTCuReduced => DryWeatherTCuReduced + WetWeatherTCuReduced ?? 0;
    public double TotalTPbReduced => DryWeatherTPbReduced + WetWeatherTPbReduced ?? 0;
    public double TotalTZnReduced => DryWeatherTZnReduced + WetWeatherTZnReduced ?? 0;
    public double TotalTSSInflow => DryWeatherTSSInflow + WetWeatherTSSInflow ?? 0;
    public double TotalTNInflow => DryWeatherTNInflow + WetWeatherTNInflow ?? 0;
    public double TotalTPInflow => DryWeatherTPInflow + WetWeatherTPInflow ?? 0;
    public double TotalFCInflow => DryWeatherFCInflow + WetWeatherFCInflow ?? 0;
    public double TotalTCuInflow => DryWeatherTCuInflow + WetWeatherTCuInflow ?? 0;
    public double TotalTPbInflow => DryWeatherTPbInflow + WetWeatherTPbInflow ?? 0;
    public double TotalTZnInflow => DryWeatherTZnInflow + WetWeatherTZnInflow ?? 0;
}