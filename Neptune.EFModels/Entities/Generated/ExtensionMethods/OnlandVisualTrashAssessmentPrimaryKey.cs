//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessment


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessment>
    {
        public OnlandVisualTrashAssessmentPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentPrimaryKey(OnlandVisualTrashAssessment onlandVisualTrashAssessment) : base(onlandVisualTrashAssessment){}

        public static implicit operator OnlandVisualTrashAssessmentPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentPrimaryKey(OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            return new OnlandVisualTrashAssessmentPrimaryKey(onlandVisualTrashAssessment);
        }
    }
}