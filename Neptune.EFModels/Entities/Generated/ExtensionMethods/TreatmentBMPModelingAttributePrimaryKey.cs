//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPModelingAttribute


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPModelingAttributePrimaryKey : EntityPrimaryKey<TreatmentBMPModelingAttribute>
    {
        public TreatmentBMPModelingAttributePrimaryKey() : base(){}
        public TreatmentBMPModelingAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPModelingAttributePrimaryKey(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute) : base(treatmentBMPModelingAttribute){}

        public static implicit operator TreatmentBMPModelingAttributePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPModelingAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPModelingAttributePrimaryKey(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
        {
            return new TreatmentBMPModelingAttributePrimaryKey(treatmentBMPModelingAttribute);
        }
    }
}