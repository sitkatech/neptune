//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeAssessmentObservationType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPTypeAssessmentObservationTypePrimaryKey : EntityPrimaryKey<TreatmentBMPTypeAssessmentObservationType>
    {
        public TreatmentBMPTypeAssessmentObservationTypePrimaryKey() : base(){}
        public TreatmentBMPTypeAssessmentObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeAssessmentObservationTypePrimaryKey(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType) : base(treatmentBMPTypeAssessmentObservationType){}

        public static implicit operator TreatmentBMPTypeAssessmentObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeAssessmentObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeAssessmentObservationTypePrimaryKey(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType)
        {
            return new TreatmentBMPTypeAssessmentObservationTypePrimaryKey(treatmentBMPTypeAssessmentObservationType);
        }
    }
}