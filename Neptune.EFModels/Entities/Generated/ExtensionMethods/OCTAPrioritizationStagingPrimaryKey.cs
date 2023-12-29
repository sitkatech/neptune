//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OCTAPrioritizationStaging


namespace Neptune.EFModels.Entities
{
    public class OCTAPrioritizationStagingPrimaryKey : EntityPrimaryKey<OCTAPrioritizationStaging>
    {
        public OCTAPrioritizationStagingPrimaryKey() : base(){}
        public OCTAPrioritizationStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OCTAPrioritizationStagingPrimaryKey(OCTAPrioritizationStaging oCTAPrioritizationStaging) : base(oCTAPrioritizationStaging){}

        public static implicit operator OCTAPrioritizationStagingPrimaryKey(int primaryKeyValue)
        {
            return new OCTAPrioritizationStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OCTAPrioritizationStagingPrimaryKey(OCTAPrioritizationStaging oCTAPrioritizationStaging)
        {
            return new OCTAPrioritizationStagingPrimaryKey(oCTAPrioritizationStaging);
        }
    }
}