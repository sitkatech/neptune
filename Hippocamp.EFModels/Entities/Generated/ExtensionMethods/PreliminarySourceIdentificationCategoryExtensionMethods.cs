//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationCategory]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PreliminarySourceIdentificationCategoryExtensionMethods
    {
        public static PreliminarySourceIdentificationCategoryDto AsDto(this PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory)
        {
            var preliminarySourceIdentificationCategoryDto = new PreliminarySourceIdentificationCategoryDto()
            {
                PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryID,
                PreliminarySourceIdentificationCategoryName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryName,
                PreliminarySourceIdentificationCategoryDisplayName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName
            };
            DoCustomMappings(preliminarySourceIdentificationCategory, preliminarySourceIdentificationCategoryDto);
            return preliminarySourceIdentificationCategoryDto;
        }

        static partial void DoCustomMappings(PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory, PreliminarySourceIdentificationCategoryDto preliminarySourceIdentificationCategoryDto);

        public static PreliminarySourceIdentificationCategorySimpleDto AsSimpleDto(this PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory)
        {
            var preliminarySourceIdentificationCategorySimpleDto = new PreliminarySourceIdentificationCategorySimpleDto()
            {
                PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryID,
                PreliminarySourceIdentificationCategoryName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryName,
                PreliminarySourceIdentificationCategoryDisplayName = preliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName
            };
            DoCustomSimpleDtoMappings(preliminarySourceIdentificationCategory, preliminarySourceIdentificationCategorySimpleDto);
            return preliminarySourceIdentificationCategorySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory, PreliminarySourceIdentificationCategorySimpleDto preliminarySourceIdentificationCategorySimpleDto);
    }
}