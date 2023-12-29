MERGE INTO dbo.MaintenanceRecordType AS Target
USING (VALUES
(1, 'Routine','Routine'),
(2, 'Corrective','Corrective')
)
AS Source (MaintenanceRecordTypeID, MaintenanceRecordTypeName, MaintenanceRecordTypeDisplayName)
ON Target.MaintenanceRecordTypeID = Source.MaintenanceRecordTypeID
WHEN MATCHED THEN
UPDATE SET
	MaintenanceRecordTypeName = Source.MaintenanceRecordTypeName,
	MaintenanceRecordTypeDisplayName = Source.MaintenanceRecordTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (MaintenanceRecordTypeID, MaintenanceRecordTypeName, MaintenanceRecordTypeDisplayName)
	VALUES (MaintenanceRecordTypeID, MaintenanceRecordTypeName, MaintenanceRecordTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;