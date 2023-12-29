//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentScore


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentScorePrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentScore>
    {
        public OnlandVisualTrashAssessmentScorePrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentScorePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentScorePrimaryKey(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore) : base(onlandVisualTrashAssessmentScore){}

        public static implicit operator OnlandVisualTrashAssessmentScorePrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentScorePrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentScorePrimaryKey(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            return new OnlandVisualTrashAssessmentScorePrimaryKey(onlandVisualTrashAssessmentScore);
        }
    }
}