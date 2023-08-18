//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPLifespanType


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPLifespanTypePrimaryKey : EntityPrimaryKey<TreatmentBMPLifespanType>
    {
        public TreatmentBMPLifespanTypePrimaryKey() : base(){}
        public TreatmentBMPLifespanTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPLifespanTypePrimaryKey(TreatmentBMPLifespanType treatmentBMPLifespanType) : base(treatmentBMPLifespanType){}

        public static implicit operator TreatmentBMPLifespanTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPLifespanTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPLifespanTypePrimaryKey(TreatmentBMPLifespanType treatmentBMPLifespanType)
        {
            return new TreatmentBMPLifespanTypePrimaryKey(treatmentBMPLifespanType);
        }
    }
}