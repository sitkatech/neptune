using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class vProjectLoadGeneratingResultExtensionMethods
{
    public static ProjectLoadGeneratingResultDto AsDto(this vProjectLoadGeneratingResult result)
    {
        var toReturn = new ProjectLoadGeneratingResultDto()
        {
            ProjectID = result.ProjectID,
            WetWeatherVolumeGenerated = result.WetWeatherVolumeGenerated ?? 0,
            WetWeatherTSSGenerated = result.WetWeatherTSSGenerated ?? 0,
            WetWeatherTNGenerated = result.WetWeatherTNGenerated ?? 0,
            WetWeatherTPGenerated = result.WetWeatherTPGenerated ?? 0,
            WetWeatherFCGenerated = result.WetWeatherFCGenerated ?? 0,
            WetWeatherTCuGenerated = result.WetWeatherTCuGenerated ?? 0,
            WetWeatherTPbGenerated = result.WetWeatherTPbGenerated ?? 0,
            WetWeatherTZnGenerated = result.WetWeatherTZnGenerated ?? 0,
            SummerDryWeatherVolumeGenerated = result.SummerDryWeatherVolumeGenerated ?? 0,
            SummerDryWeatherTSSGenerated = result.SummerDryWeatherTSSGenerated ?? 0,
            SummerDryWeatherTNGenerated = result.SummerDryWeatherTNGenerated ?? 0,
            SummerDryWeatherTPGenerated = result.SummerDryWeatherTPGenerated ?? 0,
            SummerDryWeatherFCGenerated = result.SummerDryWeatherFCGenerated ?? 0,
            SummerDryWeatherTCuGenerated = result.SummerDryWeatherTCuGenerated ?? 0,
            SummerDryWeatherTPbGenerated = result.SummerDryWeatherTPbGenerated ?? 0,
            SummerDryWeatherTZnGenerated = result.SummerDryWeatherTZnGenerated ?? 0,
            WinterDryWeatherVolumeGenerated = result.WinterDryWeatherVolumeGenerated ?? 0,
            WinterDryWeatherTSSGenerated = result.WinterDryWeatherTSSGenerated ?? 0,
            WinterDryWeatherTNGenerated = result.WinterDryWeatherTNGenerated ?? 0,
            WinterDryWeatherTPGenerated = result.WinterDryWeatherTPGenerated ?? 0,
            WinterDryWeatherFCGenerated = result.WinterDryWeatherFCGenerated ?? 0,
            WinterDryWeatherTCuGenerated = result.WinterDryWeatherTCuGenerated ?? 0,
            WinterDryWeatherTPbGenerated = result.WinterDryWeatherTPbGenerated ?? 0,
            WinterDryWeatherTZnGenerated = result.WinterDryWeatherTZnGenerated ?? 0,
        };
        return toReturn;
    }
}