//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashCaptureStatusType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TrashCaptureStatusTypeExtensionMethods
    {
        public static TrashCaptureStatusTypeDto AsDto(this TrashCaptureStatusType trashCaptureStatusType)
        {
            var trashCaptureStatusTypeDto = new TrashCaptureStatusTypeDto()
            {
                TrashCaptureStatusTypeID = trashCaptureStatusType.TrashCaptureStatusTypeID,
                TrashCaptureStatusTypeName = trashCaptureStatusType.TrashCaptureStatusTypeName,
                TrashCaptureStatusTypeDisplayName = trashCaptureStatusType.TrashCaptureStatusTypeDisplayName,
                TrashCaptureStatusTypeSortOrder = trashCaptureStatusType.TrashCaptureStatusTypeSortOrder,
                TrashCaptureStatusTypePriority = trashCaptureStatusType.TrashCaptureStatusTypePriority,
                TrashCaptureStatusTypeColorCode = trashCaptureStatusType.TrashCaptureStatusTypeColorCode
            };
            DoCustomMappings(trashCaptureStatusType, trashCaptureStatusTypeDto);
            return trashCaptureStatusTypeDto;
        }

        static partial void DoCustomMappings(TrashCaptureStatusType trashCaptureStatusType, TrashCaptureStatusTypeDto trashCaptureStatusTypeDto);

        public static TrashCaptureStatusTypeSimpleDto AsSimpleDto(this TrashCaptureStatusType trashCaptureStatusType)
        {
            var trashCaptureStatusTypeSimpleDto = new TrashCaptureStatusTypeSimpleDto()
            {
                TrashCaptureStatusTypeID = trashCaptureStatusType.TrashCaptureStatusTypeID,
                TrashCaptureStatusTypeName = trashCaptureStatusType.TrashCaptureStatusTypeName,
                TrashCaptureStatusTypeDisplayName = trashCaptureStatusType.TrashCaptureStatusTypeDisplayName,
                TrashCaptureStatusTypeSortOrder = trashCaptureStatusType.TrashCaptureStatusTypeSortOrder,
                TrashCaptureStatusTypePriority = trashCaptureStatusType.TrashCaptureStatusTypePriority,
                TrashCaptureStatusTypeColorCode = trashCaptureStatusType.TrashCaptureStatusTypeColorCode
            };
            DoCustomSimpleDtoMappings(trashCaptureStatusType, trashCaptureStatusTypeSimpleDto);
            return trashCaptureStatusTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TrashCaptureStatusType trashCaptureStatusType, TrashCaptureStatusTypeSimpleDto trashCaptureStatusTypeSimpleDto);
    }
}