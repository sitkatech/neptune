//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentPhoto


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPAssessmentPhotoPrimaryKey : EntityPrimaryKey<TreatmentBMPAssessmentPhoto>
    {
        public TreatmentBMPAssessmentPhotoPrimaryKey() : base(){}
        public TreatmentBMPAssessmentPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentPhotoPrimaryKey(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto) : base(treatmentBMPAssessmentPhoto){}

        public static implicit operator TreatmentBMPAssessmentPhotoPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentPhotoPrimaryKey(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto)
        {
            return new TreatmentBMPAssessmentPhotoPrimaryKey(treatmentBMPAssessmentPhoto);
        }
    }
}