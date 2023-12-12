//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NotificationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NotificationTypeExtensionMethods
    {
        public static NotificationTypeSimpleDto AsSimpleDto(this NotificationType notificationType)
        {
            var dto = new NotificationTypeSimpleDto()
            {
                NotificationTypeID = notificationType.NotificationTypeID,
                NotificationTypeName = notificationType.NotificationTypeName,
                NotificationTypeDisplayName = notificationType.NotificationTypeDisplayName
            };
            return dto;
        }
    }
}