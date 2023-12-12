//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentScore]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentScoreExtensionMethods
    {
        public static OnlandVisualTrashAssessmentScoreSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            var dto = new OnlandVisualTrashAssessmentScoreSimpleDto()
            {
                OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID,
                OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreName,
                OnlandVisualTrashAssessmentScoreDisplayName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreDisplayName,
                NumericValue = onlandVisualTrashAssessmentScore.NumericValue,
                TrashGenerationRate = onlandVisualTrashAssessmentScore.TrashGenerationRate
            };
            return dto;
        }
    }
}