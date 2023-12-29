//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeDataType


namespace Neptune.EFModels.Entities
{
    public class CustomAttributeDataTypePrimaryKey : EntityPrimaryKey<CustomAttributeDataType>
    {
        public CustomAttributeDataTypePrimaryKey() : base(){}
        public CustomAttributeDataTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeDataTypePrimaryKey(CustomAttributeDataType customAttributeDataType) : base(customAttributeDataType){}

        public static implicit operator CustomAttributeDataTypePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeDataTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeDataTypePrimaryKey(CustomAttributeDataType customAttributeDataType)
        {
            return new CustomAttributeDataTypePrimaryKey(customAttributeDataType);
        }
    }
}