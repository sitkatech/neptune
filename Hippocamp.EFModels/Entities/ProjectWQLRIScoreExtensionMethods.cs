using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities;

public static class ProjectWQLRIScoreExtensionMethods
{
    public static ProjectWQLRIScoreDto AsDto(this vProjectWQLRIScore vProjectWQLRIScore)
    {
        return new ProjectWQLRIScoreDto()
        {
            ProjectID = vProjectWQLRIScore.ProjectID,
            PercentReducedVolume = vProjectWQLRIScore.PercentReducedVolume,
            WeightedReductionVolume = vProjectWQLRIScore.WeightedReductionVolume,
            PercentReducedTSS = vProjectWQLRIScore.PercentReducedTSS,
            WeightedReductionTSS = vProjectWQLRIScore.WeightedReductionTSS,
            PercentReducedFC = vProjectWQLRIScore.PercentReducedFC,
            WeightedReductionBacteria = vProjectWQLRIScore.WeightedReductionBacteria,
            PercentReducedTN = vProjectWQLRIScore.PercentReducedTN,
            PercentReducedTP = vProjectWQLRIScore.PercentReducedTP,
            PercentReducedNutrients = vProjectWQLRIScore.PercentReducedNutrients,
            WeightedReductionNutrients = vProjectWQLRIScore.WeightedReductionNutrients,
            PercentReducedTPb = vProjectWQLRIScore.PercentReducedTPb,
            PercentReducedTCu = vProjectWQLRIScore.PercentReducedTCu,
            PercentReducedTZn = vProjectWQLRIScore.PercentReducedTZn,
            PercentReducedMetals = vProjectWQLRIScore.PercentReducedMetals,
            WeightedReductionMetals = vProjectWQLRIScore.WeightedReductionMetals,
        };
    }
}