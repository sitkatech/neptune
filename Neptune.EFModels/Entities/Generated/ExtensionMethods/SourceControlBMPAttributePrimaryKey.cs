//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SourceControlBMPAttribute


namespace Neptune.EFModels.Entities
{
    public class SourceControlBMPAttributePrimaryKey : EntityPrimaryKey<SourceControlBMPAttribute>
    {
        public SourceControlBMPAttributePrimaryKey() : base(){}
        public SourceControlBMPAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SourceControlBMPAttributePrimaryKey(SourceControlBMPAttribute sourceControlBMPAttribute) : base(sourceControlBMPAttribute){}

        public static implicit operator SourceControlBMPAttributePrimaryKey(int primaryKeyValue)
        {
            return new SourceControlBMPAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator SourceControlBMPAttributePrimaryKey(SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            return new SourceControlBMPAttributePrimaryKey(sourceControlBMPAttribute);
        }
    }
}