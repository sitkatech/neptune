//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentObservationType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPAssessmentObservationTypePrimaryKey : EntityPrimaryKey<TreatmentBMPAssessmentObservationType>
    {
        public TreatmentBMPAssessmentObservationTypePrimaryKey() : base(){}
        public TreatmentBMPAssessmentObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(treatmentBMPAssessmentObservationType){}

        public static implicit operator TreatmentBMPAssessmentObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return new TreatmentBMPAssessmentObservationTypePrimaryKey(treatmentBMPAssessmentObservationType);
        }
    }
}