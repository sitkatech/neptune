//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentObservationPhotoStaging


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentObservationPhotoStaging>
    {
        public OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging) : base(onlandVisualTrashAssessmentObservationPhotoStaging){}

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey(onlandVisualTrashAssessmentObservationPhotoStaging);
        }
    }
}