MERGE INTO dbo.ObservationTargetType AS Target
USING (VALUES
(1, N'PassFail', N'Observation is Pass/Fail'),
(2, N'High', N'Higher observed values result in higher score'),
(3, N'Low', N'Lower observed values result in higher score'),
(4, N'SpecificValue', N'Observed values exactly equal to the benchmark result in highest score')
)
AS Source (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName)
ON Target.ObservationTargetTypeID = Source.ObservationTargetTypeID
WHEN MATCHED THEN
UPDATE SET
	ObservationTargetTypeName = Source.ObservationTargetTypeName,
	ObservationTargetTypeDisplayName = Source.ObservationTargetTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName)
	VALUES (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
