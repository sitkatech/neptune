//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldDefinitionType


namespace Neptune.EFModels.Entities
{
    public class FieldDefinitionTypePrimaryKey : EntityPrimaryKey<FieldDefinitionType>
    {
        public FieldDefinitionTypePrimaryKey() : base(){}
        public FieldDefinitionTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldDefinitionTypePrimaryKey(FieldDefinitionType fieldDefinitionType) : base(fieldDefinitionType){}

        public static implicit operator FieldDefinitionTypePrimaryKey(int primaryKeyValue)
        {
            return new FieldDefinitionTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldDefinitionTypePrimaryKey(FieldDefinitionType fieldDefinitionType)
        {
            return new FieldDefinitionTypePrimaryKey(fieldDefinitionType);
        }
    }
}