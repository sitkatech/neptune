MERGE INTO dbo.ObservationThresholdType AS Target
USING (VALUES
(1, N'SpecificValue', N'Threshold is a specific value'),
(2, N'RelativeToBenchmark', N'Threshold is a relative percent of the benchmark value'),
(3, N'None', N'None')
)
AS Source (ObservationThresholdTypeID, ObservationThresholdTypeName, ObservationThresholdTypeDisplayName)
ON Target.ObservationThresholdTypeID = Source.ObservationThresholdTypeID
WHEN MATCHED THEN
UPDATE SET
	ObservationThresholdTypeName = Source.ObservationThresholdTypeName,
	ObservationThresholdTypeDisplayName = Source.ObservationThresholdTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ObservationThresholdTypeID, ObservationThresholdTypeName, ObservationThresholdTypeDisplayName)
	VALUES (ObservationThresholdTypeID, ObservationThresholdTypeName, ObservationThresholdTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;