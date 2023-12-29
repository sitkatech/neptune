MERGE INTO dbo.MonthsOfOperation AS Target
USING (VALUES
(1, 'Summer', 'Summer', 'summer'),
(2, 'Winter', 'Winter', 'winter'),
(3, 'Both', 'Both', 'both')
)
AS Source (MonthsOfOperationID, MonthsOfOperationName, MonthsOfOperationDisplayName, MonthsOfOperationNereidAlias)
ON Target.MonthsOfOperationID = Source.MonthsOfOperationID
WHEN MATCHED THEN
UPDATE SET
	MonthsOfOperationName = Source.MonthsOfOperationName,
	MonthsOfOperationDisplayName = Source.MonthsOfOperationDisplayName,
	MonthsOfOperationNereidAlias = Source.MonthsOfOperationNereidAlias
WHEN NOT MATCHED BY TARGET THEN
	INSERT (MonthsOfOperationID, MonthsOfOperationName, MonthsOfOperationDisplayName, MonthsOfOperationNereidAlias)
	VALUES (MonthsOfOperationID, MonthsOfOperationName, MonthsOfOperationDisplayName, MonthsOfOperationNereidAlias)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;