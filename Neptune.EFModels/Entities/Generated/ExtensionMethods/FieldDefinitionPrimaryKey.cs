//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldDefinition


namespace Neptune.EFModels.Entities
{
    public class FieldDefinitionPrimaryKey : EntityPrimaryKey<FieldDefinition>
    {
        public FieldDefinitionPrimaryKey() : base(){}
        public FieldDefinitionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldDefinitionPrimaryKey(FieldDefinition fieldDefinition) : base(fieldDefinition){}

        public static implicit operator FieldDefinitionPrimaryKey(int primaryKeyValue)
        {
            return new FieldDefinitionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldDefinitionPrimaryKey(FieldDefinition fieldDefinition)
        {
            return new FieldDefinitionPrimaryKey(fieldDefinition);
        }
    }
}