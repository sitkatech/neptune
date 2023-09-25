//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentStatusExtensionMethods
    {
        public static OnlandVisualTrashAssessmentStatusDto AsDto(this OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            var onlandVisualTrashAssessmentStatusDto = new OnlandVisualTrashAssessmentStatusDto()
            {
                OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusID,
                OnlandVisualTrashAssessmentStatusName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusName,
                OnlandVisualTrashAssessmentStatusDisplayName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName
            };
            DoCustomMappings(onlandVisualTrashAssessmentStatus, onlandVisualTrashAssessmentStatusDto);
            return onlandVisualTrashAssessmentStatusDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus, OnlandVisualTrashAssessmentStatusDto onlandVisualTrashAssessmentStatusDto);

        public static OnlandVisualTrashAssessmentStatusSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            var onlandVisualTrashAssessmentStatusSimpleDto = new OnlandVisualTrashAssessmentStatusSimpleDto()
            {
                OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusID,
                OnlandVisualTrashAssessmentStatusName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusName,
                OnlandVisualTrashAssessmentStatusDisplayName = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentStatus, onlandVisualTrashAssessmentStatusSimpleDto);
            return onlandVisualTrashAssessmentStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus, OnlandVisualTrashAssessmentStatusSimpleDto onlandVisualTrashAssessmentStatusSimpleDto);
    }
}