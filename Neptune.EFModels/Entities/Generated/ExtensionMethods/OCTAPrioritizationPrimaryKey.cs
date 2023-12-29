//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OCTAPrioritization


namespace Neptune.EFModels.Entities
{
    public class OCTAPrioritizationPrimaryKey : EntityPrimaryKey<OCTAPrioritization>
    {
        public OCTAPrioritizationPrimaryKey() : base(){}
        public OCTAPrioritizationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OCTAPrioritizationPrimaryKey(OCTAPrioritization oCTAPrioritization) : base(oCTAPrioritization){}

        public static implicit operator OCTAPrioritizationPrimaryKey(int primaryKeyValue)
        {
            return new OCTAPrioritizationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OCTAPrioritizationPrimaryKey(OCTAPrioritization oCTAPrioritization)
        {
            return new OCTAPrioritizationPrimaryKey(oCTAPrioritization);
        }
    }
}