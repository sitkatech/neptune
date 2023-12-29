//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttribute


namespace Neptune.EFModels.Entities
{
    public class CustomAttributePrimaryKey : EntityPrimaryKey<CustomAttribute>
    {
        public CustomAttributePrimaryKey() : base(){}
        public CustomAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributePrimaryKey(CustomAttribute customAttribute) : base(customAttribute){}

        public static implicit operator CustomAttributePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributePrimaryKey(CustomAttribute customAttribute)
        {
            return new CustomAttributePrimaryKey(customAttribute);
        }
    }
}