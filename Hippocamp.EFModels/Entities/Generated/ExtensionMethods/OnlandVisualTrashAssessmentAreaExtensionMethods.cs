//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentAreaExtensionMethods
    {
        public static OnlandVisualTrashAssessmentAreaDto AsDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var onlandVisualTrashAssessmentAreaDto = new OnlandVisualTrashAssessmentAreaDto()
            {
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
                StormwaterJurisdiction = onlandVisualTrashAssessmentArea.StormwaterJurisdiction.AsDto(),
                OnlandVisualTrashAssessmentBaselineScore = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore?.AsDto(),
                AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
                OnlandVisualTrashAssessmentProgressScore = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore?.AsDto()
            };
            DoCustomMappings(onlandVisualTrashAssessmentArea, onlandVisualTrashAssessmentAreaDto);
            return onlandVisualTrashAssessmentAreaDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, OnlandVisualTrashAssessmentAreaDto onlandVisualTrashAssessmentAreaDto);

        public static OnlandVisualTrashAssessmentAreaSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var onlandVisualTrashAssessmentAreaSimpleDto = new OnlandVisualTrashAssessmentAreaSimpleDto()
            {
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
                StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
                OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID,
                AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
                OnlandVisualTrashAssessmentProgressScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentArea, onlandVisualTrashAssessmentAreaSimpleDto);
            return onlandVisualTrashAssessmentAreaSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, OnlandVisualTrashAssessmentAreaSimpleDto onlandVisualTrashAssessmentAreaSimpleDto);
    }
}