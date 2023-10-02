namespace Neptune.EFModels.Entities;

public partial class FieldDefinition
{
    public FieldDefinition()
    {

    }
    public FieldDefinition(int fieldDefinitionID, string fieldDefinitionDataValue)
    {
        FieldDefinitionID = fieldDefinitionID;
        FieldDefinitionValue = fieldDefinitionDataValue;
    }
}