//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeType


namespace Neptune.EFModels.Entities
{
    public class CustomAttributeTypePrimaryKey : EntityPrimaryKey<CustomAttributeType>
    {
        public CustomAttributeTypePrimaryKey() : base(){}
        public CustomAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeTypePrimaryKey(CustomAttributeType customAttributeType) : base(customAttributeType){}

        public static implicit operator CustomAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeTypePrimaryKey(CustomAttributeType customAttributeType)
        {
            return new CustomAttributeTypePrimaryKey(customAttributeType);
        }
    }
}