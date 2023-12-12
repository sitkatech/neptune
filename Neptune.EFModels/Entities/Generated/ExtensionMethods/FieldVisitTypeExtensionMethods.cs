//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldVisitTypeExtensionMethods
    {

        public static FieldVisitTypeSimpleDto AsSimpleDto(this FieldVisitType fieldVisitType)
        {
            var fieldVisitTypeSimpleDto = new FieldVisitTypeSimpleDto()
            {
                FieldVisitTypeID = fieldVisitType.FieldVisitTypeID,
                FieldVisitTypeName = fieldVisitType.FieldVisitTypeName,
                FieldVisitTypeDisplayName = fieldVisitType.FieldVisitTypeDisplayName
            };
            DoCustomSimpleDtoMappings(fieldVisitType, fieldVisitTypeSimpleDto);
            return fieldVisitTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldVisitType fieldVisitType, FieldVisitTypeSimpleDto fieldVisitTypeSimpleDto);
    }
}