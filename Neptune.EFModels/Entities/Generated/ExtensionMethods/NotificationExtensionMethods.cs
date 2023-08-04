//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Notification]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NotificationExtensionMethods
    {
        public static NotificationDto AsDto(this Notification notification)
        {
            var notificationDto = new NotificationDto()
            {
                NotificationID = notification.NotificationID,
                NotificationType = notification.NotificationType.AsDto(),
                Person = notification.Person.AsDto(),
                NotificationDate = notification.NotificationDate
            };
            DoCustomMappings(notification, notificationDto);
            return notificationDto;
        }

        static partial void DoCustomMappings(Notification notification, NotificationDto notificationDto);

        public static NotificationSimpleDto AsSimpleDto(this Notification notification)
        {
            var notificationSimpleDto = new NotificationSimpleDto()
            {
                NotificationID = notification.NotificationID,
                NotificationTypeID = notification.NotificationTypeID,
                PersonID = notification.PersonID,
                NotificationDate = notification.NotificationDate
            };
            DoCustomSimpleDtoMappings(notification, notificationSimpleDto);
            return notificationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Notification notification, NotificationSimpleDto notificationSimpleDto);
    }
}