//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
namespace Neptune.EFModels.Entities
{
    public partial class OnlandVisualTrashAssessment
    {
        public OnlandVisualTrashAssessmentStatus OnlandVisualTrashAssessmentStatus => OnlandVisualTrashAssessmentStatus.AllLookupDictionary[OnlandVisualTrashAssessmentStatusID];
        public OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentScore => OnlandVisualTrashAssessmentScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentScoreID.Value] : null;
    }
}