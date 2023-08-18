//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentObservation


namespace Neptune.EFModels.Entities
{
    public class OnlandVisualTrashAssessmentObservationPrimaryKey : EntityPrimaryKey<OnlandVisualTrashAssessmentObservation>
    {
        public OnlandVisualTrashAssessmentObservationPrimaryKey() : base(){}
        public OnlandVisualTrashAssessmentObservationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentObservationPrimaryKey(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation) : base(onlandVisualTrashAssessmentObservation){}

        public static implicit operator OnlandVisualTrashAssessmentObservationPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentObservationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentObservationPrimaryKey(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            return new OnlandVisualTrashAssessmentObservationPrimaryKey(onlandVisualTrashAssessmentObservation);
        }
    }
}