//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SourceControlBMPAttributeCategory


namespace Neptune.EFModels.Entities
{
    public class SourceControlBMPAttributeCategoryPrimaryKey : EntityPrimaryKey<SourceControlBMPAttributeCategory>
    {
        public SourceControlBMPAttributeCategoryPrimaryKey() : base(){}
        public SourceControlBMPAttributeCategoryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SourceControlBMPAttributeCategoryPrimaryKey(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory) : base(sourceControlBMPAttributeCategory){}

        public static implicit operator SourceControlBMPAttributeCategoryPrimaryKey(int primaryKeyValue)
        {
            return new SourceControlBMPAttributeCategoryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator SourceControlBMPAttributeCategoryPrimaryKey(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory)
        {
            return new SourceControlBMPAttributeCategoryPrimaryKey(sourceControlBMPAttributeCategory);
        }
    }
}