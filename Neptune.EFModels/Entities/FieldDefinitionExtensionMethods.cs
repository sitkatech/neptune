using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class FieldDefinitionExtensionMethods
{
    public static FieldDefinitionDto AsDto(this FieldDefinition fieldDefinition)
    {
        var fieldDefinitionDto = new FieldDefinitionDto()
        {
            FieldDefinitionID = fieldDefinition.FieldDefinitionID,
            FieldDefinitionType = fieldDefinition.FieldDefinitionType.AsSimpleDto(),
            FieldDefinitionValue = fieldDefinition.FieldDefinitionValue
        };
        return fieldDefinitionDto;
    }
}