//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NotificationType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class NotificationTypeExtensionMethods
    {
        public static NotificationTypeDto AsDto(this NotificationType notificationType)
        {
            var notificationTypeDto = new NotificationTypeDto()
            {
                NotificationTypeID = notificationType.NotificationTypeID,
                NotificationTypeName = notificationType.NotificationTypeName,
                NotificationTypeDisplayName = notificationType.NotificationTypeDisplayName
            };
            DoCustomMappings(notificationType, notificationTypeDto);
            return notificationTypeDto;
        }

        static partial void DoCustomMappings(NotificationType notificationType, NotificationTypeDto notificationTypeDto);

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