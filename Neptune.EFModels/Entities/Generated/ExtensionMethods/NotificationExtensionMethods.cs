//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Notification]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NotificationExtensionMethods
    {
        public static NotificationSimpleDto AsSimpleDto(this Notification notification)
        {
            var dto = new NotificationSimpleDto()
            {
                NotificationID = notification.NotificationID,
                NotificationTypeID = notification.NotificationTypeID,
                PersonID = notification.PersonID,
                NotificationDate = notification.NotificationDate
            };
            return dto;
        }
    }
}