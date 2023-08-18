//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPImage


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPImagePrimaryKey : EntityPrimaryKey<TreatmentBMPImage>
    {
        public TreatmentBMPImagePrimaryKey() : base(){}
        public TreatmentBMPImagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPImagePrimaryKey(TreatmentBMPImage treatmentBMPImage) : base(treatmentBMPImage){}

        public static implicit operator TreatmentBMPImagePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPImagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPImagePrimaryKey(TreatmentBMPImage treatmentBMPImage)
        {
            return new TreatmentBMPImagePrimaryKey(treatmentBMPImage);
        }
    }
}