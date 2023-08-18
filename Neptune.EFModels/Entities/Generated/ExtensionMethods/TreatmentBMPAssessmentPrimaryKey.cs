//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessment


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPAssessmentPrimaryKey : EntityPrimaryKey<TreatmentBMPAssessment>
    {
        public TreatmentBMPAssessmentPrimaryKey() : base(){}
        public TreatmentBMPAssessmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentPrimaryKey(TreatmentBMPAssessment treatmentBMPAssessment) : base(treatmentBMPAssessment){}

        public static implicit operator TreatmentBMPAssessmentPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentPrimaryKey(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return new TreatmentBMPAssessmentPrimaryKey(treatmentBMPAssessment);
        }
    }
}