//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinitionType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class FieldDefinitionTypeExtensionMethods
    {
        public static FieldDefinitionTypeDto AsDto(this FieldDefinitionType fieldDefinitionType)
        {
            var fieldDefinitionTypeDto = new FieldDefinitionTypeDto()
            {
                FieldDefinitionTypeID = fieldDefinitionType.FieldDefinitionTypeID,
                FieldDefinitionTypeName = fieldDefinitionType.FieldDefinitionTypeName,
                FieldDefinitionTypeDisplayName = fieldDefinitionType.FieldDefinitionTypeDisplayName
            };
            DoCustomMappings(fieldDefinitionType, fieldDefinitionTypeDto);
            return fieldDefinitionTypeDto;
        }

        static partial void DoCustomMappings(FieldDefinitionType fieldDefinitionType, FieldDefinitionTypeDto fieldDefinitionTypeDto);

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