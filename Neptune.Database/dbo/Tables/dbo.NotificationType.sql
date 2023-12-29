CREATE TABLE [dbo].[NotificationType](
	[NotificationTypeID] [int] NOT NULL CONSTRAINT [PK_NotificationType_NotificationTypeID] PRIMARY KEY,
	[NotificationTypeName] [varchar](100) CONSTRAINT [AK_NotificationType_NotificationTypeName] UNIQUE,
	[NotificationTypeDisplayName] [varchar](100) CONSTRAINT [AK_NotificationType_NotificationTypeDisplayName] UNIQUE
)
