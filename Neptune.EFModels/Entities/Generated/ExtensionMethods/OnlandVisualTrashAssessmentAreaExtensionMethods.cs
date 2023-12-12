//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentAreaExtensionMethods
    {
        public static OnlandVisualTrashAssessmentAreaSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var dto = new OnlandVisualTrashAssessmentAreaSimpleDto()
            {
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
                StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
                OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID,
                AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
                OnlandVisualTrashAssessmentProgressScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID
            };
            return dto;
        }
    }
}