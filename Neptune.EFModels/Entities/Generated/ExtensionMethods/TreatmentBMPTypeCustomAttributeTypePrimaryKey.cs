//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeCustomAttributeType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPTypeCustomAttributeTypePrimaryKey : EntityPrimaryKey<TreatmentBMPTypeCustomAttributeType>
    {
        public TreatmentBMPTypeCustomAttributeTypePrimaryKey() : base(){}
        public TreatmentBMPTypeCustomAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeCustomAttributeTypePrimaryKey(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType) : base(treatmentBMPTypeCustomAttributeType){}

        public static implicit operator TreatmentBMPTypeCustomAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeCustomAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeCustomAttributeTypePrimaryKey(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            return new TreatmentBMPTypeCustomAttributeTypePrimaryKey(treatmentBMPTypeCustomAttributeType);
        }
    }
}