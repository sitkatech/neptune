//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPTypePrimaryKey : EntityPrimaryKey<TreatmentBMPType>
    {
        public TreatmentBMPTypePrimaryKey() : base(){}
        public TreatmentBMPTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypePrimaryKey(TreatmentBMPType treatmentBMPType) : base(treatmentBMPType){}

        public static implicit operator TreatmentBMPTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypePrimaryKey(TreatmentBMPType treatmentBMPType)
        {
            return new TreatmentBMPTypePrimaryKey(treatmentBMPType);
        }
    }
}