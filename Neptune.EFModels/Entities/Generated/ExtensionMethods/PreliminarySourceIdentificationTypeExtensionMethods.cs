//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PreliminarySourceIdentificationTypeExtensionMethods
    {
        public static PreliminarySourceIdentificationTypeSimpleDto AsSimpleDto(this PreliminarySourceIdentificationType preliminarySourceIdentificationType)
        {
            var dto = new PreliminarySourceIdentificationTypeSimpleDto()
            {
                PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID,
                PreliminarySourceIdentificationTypeName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeName,
                PreliminarySourceIdentificationTypeDisplayName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName,
                PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationType.PreliminarySourceIdentificationCategoryID
            };
            return dto;
        }
    }
}