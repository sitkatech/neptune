//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Notification]
namespace Neptune.EFModels.Entities
{
    public partial class Notification : IHavePrimaryKey
    {
        public int PrimaryKey => NotificationID;
        public NotificationType NotificationType => NotificationType.AllLookupDictionary[NotificationTypeID];

        public static class FieldLengths
        {

        }
    }
}