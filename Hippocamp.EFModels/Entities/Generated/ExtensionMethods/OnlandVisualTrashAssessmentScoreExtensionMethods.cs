//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentScore]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentScoreExtensionMethods
    {
        public static OnlandVisualTrashAssessmentScoreDto AsDto(this OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            var onlandVisualTrashAssessmentScoreDto = new OnlandVisualTrashAssessmentScoreDto()
            {
                OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID,
                OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreName,
                OnlandVisualTrashAssessmentScoreDisplayName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreDisplayName,
                NumericValue = onlandVisualTrashAssessmentScore.NumericValue,
                TrashGenerationRate = onlandVisualTrashAssessmentScore.TrashGenerationRate
            };
            DoCustomMappings(onlandVisualTrashAssessmentScore, onlandVisualTrashAssessmentScoreDto);
            return onlandVisualTrashAssessmentScoreDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore, OnlandVisualTrashAssessmentScoreDto onlandVisualTrashAssessmentScoreDto);

        public static OnlandVisualTrashAssessmentScoreSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            var onlandVisualTrashAssessmentScoreSimpleDto = new OnlandVisualTrashAssessmentScoreSimpleDto()
            {
                OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID,
                OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreName,
                OnlandVisualTrashAssessmentScoreDisplayName = onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreDisplayName,
                NumericValue = onlandVisualTrashAssessmentScore.NumericValue,
                TrashGenerationRate = onlandVisualTrashAssessmentScore.TrashGenerationRate
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentScore, onlandVisualTrashAssessmentScoreSimpleDto);
            return onlandVisualTrashAssessmentScoreSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore, OnlandVisualTrashAssessmentScoreSimpleDto onlandVisualTrashAssessmentScoreSimpleDto);
    }
}