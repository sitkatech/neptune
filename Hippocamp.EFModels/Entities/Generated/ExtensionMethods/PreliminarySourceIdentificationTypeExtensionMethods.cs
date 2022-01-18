//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PreliminarySourceIdentificationTypeExtensionMethods
    {
        public static PreliminarySourceIdentificationTypeDto AsDto(this PreliminarySourceIdentificationType preliminarySourceIdentificationType)
        {
            var preliminarySourceIdentificationTypeDto = new PreliminarySourceIdentificationTypeDto()
            {
                PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID,
                PreliminarySourceIdentificationTypeName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeName,
                PreliminarySourceIdentificationTypeDisplayName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName,
                PreliminarySourceIdentificationCategory = preliminarySourceIdentificationType.PreliminarySourceIdentificationCategory.AsDto()
            };
            DoCustomMappings(preliminarySourceIdentificationType, preliminarySourceIdentificationTypeDto);
            return preliminarySourceIdentificationTypeDto;
        }

        static partial void DoCustomMappings(PreliminarySourceIdentificationType preliminarySourceIdentificationType, PreliminarySourceIdentificationTypeDto preliminarySourceIdentificationTypeDto);

        public static PreliminarySourceIdentificationTypeSimpleDto AsSimpleDto(this PreliminarySourceIdentificationType preliminarySourceIdentificationType)
        {
            var preliminarySourceIdentificationTypeSimpleDto = new PreliminarySourceIdentificationTypeSimpleDto()
            {
                PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID,
                PreliminarySourceIdentificationTypeName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeName,
                PreliminarySourceIdentificationTypeDisplayName = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName,
                PreliminarySourceIdentificationCategoryID = preliminarySourceIdentificationType.PreliminarySourceIdentificationCategoryID
            };
            DoCustomSimpleDtoMappings(preliminarySourceIdentificationType, preliminarySourceIdentificationTypeSimpleDto);
            return preliminarySourceIdentificationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PreliminarySourceIdentificationType preliminarySourceIdentificationType, PreliminarySourceIdentificationTypeSimpleDto preliminarySourceIdentificationTypeSimpleDto);
    }
}