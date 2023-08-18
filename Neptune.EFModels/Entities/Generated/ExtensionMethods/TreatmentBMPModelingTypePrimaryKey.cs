//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPModelingType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPModelingTypePrimaryKey : EntityPrimaryKey<TreatmentBMPModelingType>
    {
        public TreatmentBMPModelingTypePrimaryKey() : base(){}
        public TreatmentBMPModelingTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPModelingTypePrimaryKey(TreatmentBMPModelingType treatmentBMPModelingType) : base(treatmentBMPModelingType){}

        public static implicit operator TreatmentBMPModelingTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPModelingTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPModelingTypePrimaryKey(TreatmentBMPModelingType treatmentBMPModelingType)
        {
            return new TreatmentBMPModelingTypePrimaryKey(treatmentBMPModelingType);
        }
    }
}