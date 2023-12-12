//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentStatusExtensionMethods
    {
        public static OnlandVisualTrashAssessmentStatusSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            var dto = new OnlandVisualTrashAssessmentStatusSimpleDto()
            {
                OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusID,
                OnlandVisualTrashAssessmentStatusName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusName,
                OnlandVisualTrashAssessmentStatusDisplayName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName
            };
            return dto;
        }
    }
}