MERGE INTO dbo.RegionalSubbasinRevisionRequestStatus AS Target
USING (VALUES
(1, 'Open', 'Open'),
(2, 'Closed', 'Closed')
)
AS Source (RegionalSubbasinRevisionRequestStatusID, RegionalSubbasinRevisionRequestStatusName, RegionalSubbasinRevisionRequestStatusDisplayName)
ON Target.RegionalSubbasinRevisionRequestStatusID = Source.RegionalSubbasinRevisionRequestStatusID
WHEN MATCHED THEN
UPDATE SET
	RegionalSubbasinRevisionRequestStatusName = Source.RegionalSubbasinRevisionRequestStatusName,
	RegionalSubbasinRevisionRequestStatusDisplayName = Source.RegionalSubbasinRevisionRequestStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (RegionalSubbasinRevisionRequestStatusID, RegionalSubbasinRevisionRequestStatusName, RegionalSubbasinRevisionRequestStatusDisplayName)
	VALUES (RegionalSubbasinRevisionRequestStatusID, RegionalSubbasinRevisionRequestStatusName, RegionalSubbasinRevisionRequestStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;