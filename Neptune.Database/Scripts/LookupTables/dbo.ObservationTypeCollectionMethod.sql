MERGE INTO dbo.ObservationTypeCollectionMethod AS Target
USING (VALUES
(1, N'DiscreteValue', N'Discrete Value Observation', 'Observation is measured as one or many discrete values (e.g. time, height).'),
--(2, N'Rate', N'Rate-based Observation', 'Observation is measured as one or many rates values or as time/value pairs (e.g. infiltration rate or infiltrometer readings at elapsed time intervals).'),
(3, N'PassFail', N'Pass/Fail Observation', 'Observation is recorded as Pass/Fail (e.g. presence of standing water).'),
(4, N'Percentage', N'Percent-based Observation', 'Observation is measured as one or more percent values that total to less than 100% (e.g. percent coverage of key species).')
)
AS Source (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, ObservationTypeCollectionMethodDescription)
ON Target.ObservationTypeCollectionMethodID = Source.ObservationTypeCollectionMethodID
WHEN MATCHED THEN
UPDATE SET
	ObservationTypeCollectionMethodName = Source.ObservationTypeCollectionMethodName,
	ObservationTypeCollectionMethodDisplayName = Source.ObservationTypeCollectionMethodDisplayName,
	ObservationTypeCollectionMethodDescription = Source.ObservationTypeCollectionMethodDescription
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, ObservationTypeCollectionMethodDescription)
	VALUES (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, ObservationTypeCollectionMethodDescription)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
