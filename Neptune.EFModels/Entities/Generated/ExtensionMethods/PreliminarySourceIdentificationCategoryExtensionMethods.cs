//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationCategory]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PreliminarySourceIdentificationCategoryExtensionMethods
    {
        public static PreliminarySourceIdentificationCategorySimpleDto AsSimpleDto(this PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory)
        {
            var dto = new PreliminarySourceIdentificationCategorySimpleDto()
            {
                PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryID,
                PreliminarySourceIdentificationCategoryName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryName,
                PreliminarySourceIdentificationCategoryDisplayName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName
            };
            return dto;
        }
    }
}