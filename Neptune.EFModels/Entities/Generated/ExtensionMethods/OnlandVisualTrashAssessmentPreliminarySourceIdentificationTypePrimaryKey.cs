//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentPreliminarySourceIdentificationType


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>
    {
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType) : base(onlandVisualTrashAssessmentPreliminarySourceIdentificationType){}

        public static implicit operator OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            return new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(onlandVisualTrashAssessmentPreliminarySourceIdentificationType);
        }
    }
}