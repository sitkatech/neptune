//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Notification]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class NotificationDto
    {
        public int NotificationID { get; set; }
        public NotificationTypeDto NotificationType { get; set; }
        public PersonDto Person { get; set; }
        public DateTime NotificationDate { get; set; }
    }

    public partial class NotificationSimpleDto
    {
        public int NotificationID { get; set; }
        public System.Int32 NotificationTypeID { get; set; }
        public System.Int32 PersonID { get; set; }
        public DateTime NotificationDate { get; set; }
    }

}