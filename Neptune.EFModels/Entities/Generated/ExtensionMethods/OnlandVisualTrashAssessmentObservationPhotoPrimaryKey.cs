//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentObservationPhoto


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentObservationPhotoPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentObservationPhoto>
    {
        public OnlandVisualTrashAssessmentObservationPhotoPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto) : base(onlandVisualTrashAssessmentObservationPhoto){}

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(onlandVisualTrashAssessmentObservationPhoto);
        }
    }
}