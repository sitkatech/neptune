CREATE TABLE [dbo].[Notification](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Notification_NotificationID] PRIMARY KEY,
	[NotificationTypeID] [int] NOT NULL CONSTRAINT [FK_Notification_NotificationType_NotificationTypeID] FOREIGN KEY REFERENCES [dbo].[NotificationType] ([NotificationTypeID]),
	[PersonID] [int] NOT NULL CONSTRAINT [FK_Notification_Person_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[NotificationDate] [datetime] NOT NULL
)