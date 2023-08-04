MERGE INTO dbo.FieldVisitType AS Target
USING (VALUES
(1, 'DryWeather', 'Dry Weather'),
(2, 'WetWeather', 'Wet Weather'),
(3, 'PostStormAssessment', 'Post-Storm Assessment')
)
AS Source (FieldVisitTypeID, FieldVisitTypeName, FieldVisitTypeDisplayName)
ON Target.FieldVisitTypeID = Source.FieldVisitTypeID
WHEN MATCHED THEN
UPDATE SET
	FieldVisitTypeName = Source.FieldVisitTypeName,
	FieldVisitTypeDisplayName = Source.FieldVisitTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (FieldVisitTypeID, FieldVisitTypeName, FieldVisitTypeDisplayName)
	VALUES (FieldVisitTypeID, FieldVisitTypeName, FieldVisitTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;