//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentAreaStaging


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentAreaStagingPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentAreaStaging>
    {
        public OnlandVisualTrashAssessmentAreaStagingPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentAreaStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentAreaStagingPrimaryKey(OnlandVisualTrashAssessmentAreaStaging onlandVisualTrashAssessmentAreaStaging) : base(onlandVisualTrashAssessmentAreaStaging){}

        public static implicit operator OnlandVisualTrashAssessmentAreaStagingPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentAreaStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentAreaStagingPrimaryKey(OnlandVisualTrashAssessmentAreaStaging onlandVisualTrashAssessmentAreaStaging)
        {
            return new OnlandVisualTrashAssessmentAreaStagingPrimaryKey(onlandVisualTrashAssessmentAreaStaging);
        }
    }
}