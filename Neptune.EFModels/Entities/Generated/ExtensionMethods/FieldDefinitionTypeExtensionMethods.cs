//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinitionType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldDefinitionTypeExtensionMethods
    {

        public static FieldDefinitionTypeSimpleDto AsSimpleDto(this FieldDefinitionType fieldDefinitionType)
        {
            var fieldDefinitionTypeSimpleDto = new FieldDefinitionTypeSimpleDto()
            {
                FieldDefinitionTypeID = fieldDefinitionType.FieldDefinitionTypeID,
                FieldDefinitionTypeName = fieldDefinitionType.FieldDefinitionTypeName,
                FieldDefinitionTypeDisplayName = fieldDefinitionType.FieldDefinitionTypeDisplayName
            };
            DoCustomSimpleDtoMappings(fieldDefinitionType, fieldDefinitionTypeSimpleDto);
            return fieldDefinitionTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldDefinitionType fieldDefinitionType, FieldDefinitionTypeSimpleDto fieldDefinitionTypeSimpleDto);
    }
}