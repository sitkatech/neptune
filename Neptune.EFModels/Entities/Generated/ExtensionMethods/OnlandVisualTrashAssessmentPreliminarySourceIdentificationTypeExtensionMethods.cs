//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeExtensionMethods
    {
        public static OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto AsDto(this OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto = new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto()
            {
                OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID,
                OnlandVisualTrashAssessment = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessment.AsDto(),
                PreliminarySourceIdentificationType = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationType.AsDto(),
                ExplanationIfTypeIsOther = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.ExplanationIfTypeIsOther
            };
            DoCustomMappings(onlandVisualTrashAssessmentPreliminarySourceIdentificationType, onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto);
            return onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType, OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeDto);

        public static OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto = new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto()
            {
                OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID,
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessmentID,
                PreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID,
                ExplanationIfTypeIsOther = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.ExplanationIfTypeIsOther
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentPreliminarySourceIdentificationType, onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto);
            return onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType, OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto);
    }
}