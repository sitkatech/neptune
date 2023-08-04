MERGE INTO dbo.DelineationType AS Target
USING (VALUES
(1, 'Centralized', 'Centralized'),
(2, 'Distributed', 'Distributed')
)
AS Source (DelineationTypeID, DelineationTypeName, DelineationTypeDisplayName)
ON Target.DelineationTypeID = Source.DelineationTypeID
WHEN MATCHED THEN
UPDATE SET
	DelineationTypeName = Source.DelineationTypeName,
	DelineationTypeDisplayName = Source.DelineationTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (DelineationTypeID, DelineationTypeName, DelineationTypeDisplayName)
	VALUES (DelineationTypeID, DelineationTypeName, DelineationTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;