//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeExtensionMethods
    {
        public static OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            var dto = new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeSimpleDto()
            {
                OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID,
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessmentID,
                PreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID,
                ExplanationIfTypeIsOther = onlandVisualTrashAssessmentPreliminarySourceIdentificationType.ExplanationIfTypeIsOther
            };
            return dto;
        }
    }
}