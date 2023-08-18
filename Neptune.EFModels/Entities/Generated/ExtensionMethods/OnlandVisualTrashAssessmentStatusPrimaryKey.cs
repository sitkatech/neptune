//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentStatus


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentStatusPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentStatus>
    {
        public OnlandVisualTrashAssessmentStatusPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentStatusPrimaryKey(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus) : base(onlandVisualTrashAssessmentStatus){}

        public static implicit operator OnlandVisualTrashAssessmentStatusPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentStatusPrimaryKey(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            return new OnlandVisualTrashAssessmentStatusPrimaryKey(onlandVisualTrashAssessmentStatus);
        }
    }
}