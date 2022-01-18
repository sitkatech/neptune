//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NotificationType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class NotificationTypeDto
    {
        public int NotificationTypeID { get; set; }
        public string NotificationTypeName { get; set; }
        public string NotificationTypeDisplayName { get; set; }
    }

    public partial class NotificationTypeSimpleDto
    {
        public int NotificationTypeID { get; set; }
        public string NotificationTypeName { get; set; }
        public string NotificationTypeDisplayName { get; set; }
    }

}