namespace Neptune.Models.DataTransferObjects;

public class ProjectLoadGeneratingResultDto
{
    public int ProjectID { get; set; }
    public int? DelineationID { get; set; }
    public double? WetWeatherVolumeGenerated { get; set; }
    public double? WetWeatherTSSGenerated { get; set; }
    public double? WetWeatherTNGenerated { get; set; }
    public double? WetWeatherTPGenerated { get; set; }
    public double? WetWeatherFCGenerated { get; set; }
    public double? WetWeatherTCuGenerated { get; set; }
    public double? WetWeatherTPbGenerated { get; set; }
    public double? WetWeatherTZnGenerated { get; set; }
    public double? SummerDryWeatherVolumeGenerated { get; set; }
    public double? SummerDryWeatherTSSGenerated { get; set; }
    public double? SummerDryWeatherTNGenerated { get; set; }
    public double? SummerDryWeatherTPGenerated { get; set; }
    public double? SummerDryWeatherFCGenerated { get; set; }
    public double? SummerDryWeatherTCuGenerated { get; set; }
    public double? SummerDryWeatherTPbGenerated { get; set; }
    public double? SummerDryWeatherTZnGenerated { get; set; }
    public double? WinterDryWeatherVolumeGenerated { get; set; }
    public double? WinterDryWeatherTSSGenerated { get; set; }
    public double? WinterDryWeatherTNGenerated { get; set; }
    public double? WinterDryWeatherTPGenerated { get; set; }
    public double? WinterDryWeatherFCGenerated { get; set; }
    public double? WinterDryWeatherTCuGenerated { get; set; }
    public double? WinterDryWeatherTPbGenerated { get; set; }
    public double? WinterDryWeatherTZnGenerated { get; set; }

    public double? DryWeatherVolumeGenerated => SummerDryWeatherVolumeGenerated + WinterDryWeatherVolumeGenerated;
    public double? DryWeatherTSSGenerated => SummerDryWeatherTSSGenerated + WinterDryWeatherTSSGenerated;
    public double? DryWeatherTNGenerated => SummerDryWeatherTNGenerated + WinterDryWeatherTNGenerated;
    public double? DryWeatherTPGenerated => SummerDryWeatherTPGenerated + WinterDryWeatherTPGenerated;
    public double? DryWeatherFCGenerated => SummerDryWeatherFCGenerated + WinterDryWeatherFCGenerated;
    public double? DryWeatherTCuGenerated => SummerDryWeatherTCuGenerated + WinterDryWeatherTCuGenerated;
    public double? DryWeatherTPbGenerated => SummerDryWeatherTPbGenerated + WinterDryWeatherTPbGenerated;
    public double? DryWeatherTZnGenerated => SummerDryWeatherTZnGenerated + WinterDryWeatherTZnGenerated;
}                