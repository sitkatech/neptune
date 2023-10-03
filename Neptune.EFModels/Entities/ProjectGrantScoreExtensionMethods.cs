using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class ProjectGrantScoreExtensionMethods
{
    public static ProjectGrantScoreDto AsDto(this vProjectGrantScore vProjectGrantScore)
    {
        return new ProjectGrantScoreDto()
        {
            ProjectID = vProjectGrantScore.ProjectID,
            ProjectArea = vProjectGrantScore.ProjectArea,
            PollutantVolume = vProjectGrantScore.PollutantVolume,
            PollutantMetals = vProjectGrantScore.PollutantMetals,
            PollutantBacteria = vProjectGrantScore.PollutantBacteria,
            PollutantNutrients = vProjectGrantScore.PollutantNutrients,
            PollutantTSS = vProjectGrantScore.PollutantTSS,
            TPI = vProjectGrantScore.TPI,
            SEA = vProjectGrantScore.SEA,
            DryWeatherWQLRI = vProjectGrantScore.DryWeatherWQLRI,
            WetWeatherWQLRI = vProjectGrantScore.WetWeatherWQLRI,
            Watersheds = vProjectGrantScore.Watersheds,
        };
    }
}