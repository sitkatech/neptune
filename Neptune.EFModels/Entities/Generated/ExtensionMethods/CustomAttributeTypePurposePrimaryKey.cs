//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeTypePurpose


namespace Neptune.EFModels.Entities
{
    public class CustomAttributeTypePurposePrimaryKey : EntityPrimaryKey<CustomAttributeTypePurpose>
    {
        public CustomAttributeTypePurposePrimaryKey() : base(){}
        public CustomAttributeTypePurposePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeTypePurposePrimaryKey(CustomAttributeTypePurpose customAttributeTypePurpose) : base(customAttributeTypePurpose){}

        public static implicit operator CustomAttributeTypePurposePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeTypePurposePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeTypePurposePrimaryKey(CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            return new CustomAttributeTypePurposePrimaryKey(customAttributeTypePurpose);
        }
    }
}