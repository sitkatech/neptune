//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPAssessmentTypePrimaryKey : EntityPrimaryKey<TreatmentBMPAssessmentType>
    {
        public TreatmentBMPAssessmentTypePrimaryKey() : base(){}
        public TreatmentBMPAssessmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentTypePrimaryKey(TreatmentBMPAssessmentType treatmentBMPAssessmentType) : base(treatmentBMPAssessmentType){}

        public static implicit operator TreatmentBMPAssessmentTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentTypePrimaryKey(TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            return new TreatmentBMPAssessmentTypePrimaryKey(treatmentBMPAssessmentType);
        }
    }
}