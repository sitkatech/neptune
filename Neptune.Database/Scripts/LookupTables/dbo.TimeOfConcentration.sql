MERGE INTO dbo.TimeOfConcentration AS Target
USING (VALUES
(1, 'FiveMinutes', '5'),
(2, 'TenMinutes', '10'),
(3, 'FifteenMinutes', '15'),
(4, 'TwentyMinutes', '20'),
(5, 'ThirtyMinutes', '30'),
(6, 'FortyFiveMinutes', '45'),
(7, 'SixtyMinutes', '60')
)
AS Source (TimeOfConcentrationID, TimeOfConcentrationName, TimeOfConcentrationDisplayName)
ON Target.TimeOfConcentrationID = Source.TimeOfConcentrationID
WHEN MATCHED THEN
UPDATE SET
	TimeOfConcentrationName = Source.TimeOfConcentrationName,
	TimeOfConcentrationDisplayName = Source.TimeOfConcentrationDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (TimeOfConcentrationID, TimeOfConcentrationName, TimeOfConcentrationDisplayName)
	VALUES (TimeOfConcentrationID, TimeOfConcentrationName, TimeOfConcentrationDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
