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
            var dto = new FieldVisitTypeSimpleDto()
            {
                FieldVisitTypeID = fieldVisitType.FieldVisitTypeID,
                FieldVisitTypeName = fieldVisitType.FieldVisitTypeName,
                FieldVisitTypeDisplayName = fieldVisitType.FieldVisitTypeDisplayName
            };
            return dto;
        }
    }
}