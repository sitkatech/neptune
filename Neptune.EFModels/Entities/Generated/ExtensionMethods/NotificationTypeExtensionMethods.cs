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
            var notificationTypeSimpleDto = new NotificationTypeSimpleDto()
            {
                NotificationTypeID = notificationType.NotificationTypeID,
                NotificationTypeName = notificationType.NotificationTypeName,
                NotificationTypeDisplayName = notificationType.NotificationTypeDisplayName
            };
            DoCustomSimpleDtoMappings(notificationType, notificationTypeSimpleDto);
            return notificationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NotificationType notificationType, NotificationTypeSimpleDto notificationTypeSimpleDto);
    }
}