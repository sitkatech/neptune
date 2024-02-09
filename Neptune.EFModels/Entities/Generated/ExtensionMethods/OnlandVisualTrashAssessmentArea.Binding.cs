//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]
namespace Neptune.EFModels.Entities
{
    public partial class OnlandVisualTrashAssessmentArea : IHavePrimaryKey
    {
        public int PrimaryKey => OnlandVisualTrashAssessmentAreaID;
        public OnlandVisualTrashAssessmentScore? OnlandVisualTrashAssessmentBaselineScore => OnlandVisualTrashAssessmentBaselineScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentBaselineScoreID.Value] : null;
        public OnlandVisualTrashAssessmentScore? OnlandVisualTrashAssessmentProgressScore => OnlandVisualTrashAssessmentProgressScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentProgressScoreID.Value] : null;

        public static class FieldLengths
        {
            public const int OnlandVisualTrashAssessmentAreaName = 100;
            public const int AssessmentAreaDescription = 500;
        }
    }
}