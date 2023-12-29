//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Notification


namespace Neptune.EFModels.Entities
{
    public class NotificationPrimaryKey : EntityPrimaryKey<Notification>
    {
        public NotificationPrimaryKey() : base(){}
        public NotificationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NotificationPrimaryKey(Notification notification) : base(notification){}

        public static implicit operator NotificationPrimaryKey(int primaryKeyValue)
        {
            return new NotificationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NotificationPrimaryKey(Notification notification)
        {
            return new NotificationPrimaryKey(notification);
        }
    }
}