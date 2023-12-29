MERGE INTO dbo.NotificationType AS Target
USING (VALUES
(1, 'Custom', 'Custom Notification')
)
AS Source (NotificationTypeID, NotificationTypeName, NotificationTypeDisplayName)
ON Target.NotificationTypeID = Source.NotificationTypeID
WHEN MATCHED THEN
UPDATE SET
	NotificationTypeName = Source.NotificationTypeName,
	NotificationTypeDisplayName = Source.NotificationTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (NotificationTypeID, NotificationTypeName, NotificationTypeDisplayName)
	VALUES (NotificationTypeID, NotificationTypeName, NotificationTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;