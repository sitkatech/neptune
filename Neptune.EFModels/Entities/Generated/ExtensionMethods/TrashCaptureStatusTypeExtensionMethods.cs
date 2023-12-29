//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashCaptureStatusType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TrashCaptureStatusTypeExtensionMethods
    {
        public static TrashCaptureStatusTypeSimpleDto AsSimpleDto(this TrashCaptureStatusType trashCaptureStatusType)
        {
            var dto = new TrashCaptureStatusTypeSimpleDto()
            {
                TrashCaptureStatusTypeID = trashCaptureStatusType.TrashCaptureStatusTypeID,
                TrashCaptureStatusTypeName = trashCaptureStatusType.TrashCaptureStatusTypeName,
                TrashCaptureStatusTypeDisplayName = trashCaptureStatusType.TrashCaptureStatusTypeDisplayName,
                TrashCaptureStatusTypeSortOrder = trashCaptureStatusType.TrashCaptureStatusTypeSortOrder,
                TrashCaptureStatusTypePriority = trashCaptureStatusType.TrashCaptureStatusTypePriority,
                TrashCaptureStatusTypeColorCode = trashCaptureStatusType.TrashCaptureStatusTypeColorCode
            };
            return dto;
        }
    }
}