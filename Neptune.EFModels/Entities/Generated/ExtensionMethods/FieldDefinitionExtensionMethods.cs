//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldDefinitionExtensionMethods
    {
        public static FieldDefinitionDto AsDto(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionDto = new FieldDefinitionDto()
            {
                FieldDefinitionID = fieldDefinition.FieldDefinitionID,
                FieldDefinitionType = fieldDefinition.FieldDefinitionType.AsDto(),
                FieldDefinitionValue = fieldDefinition.FieldDefinitionValue
            };
            DoCustomMappings(fieldDefinition, fieldDefinitionDto);
            return fieldDefinitionDto;
        }

        static partial void DoCustomMappings(FieldDefinition fieldDefinition, FieldDefinitionDto fieldDefinitionDto);

        public static FieldDefinitionSimpleDto AsSimpleDto(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionSimpleDto = new FieldDefinitionSimpleDto()
            {
                FieldDefinitionID = fieldDefinition.FieldDefinitionID,
                FieldDefinitionTypeID = fieldDefinition.FieldDefinitionTypeID,
                FieldDefinitionValue = fieldDefinition.FieldDefinitionValue
            };
            DoCustomSimpleDtoMappings(fieldDefinition, fieldDefinitionSimpleDto);
            return fieldDefinitionSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldDefinition fieldDefinition, FieldDefinitionSimpleDto fieldDefinitionSimpleDto);
    }
}