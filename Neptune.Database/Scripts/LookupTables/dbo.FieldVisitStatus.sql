MERGE INTO dbo.FieldVisitStatus AS Target
USING (VALUES
(1, 'InProgress', 'In Progress'),
(2, 'Complete', 'Complete'),
(3, 'Unresolved', 'Unresolved'),
(4, 'ReturnedToEdit', 'Returned to Edit')
)
AS Source (FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
ON Target.FieldVisitStatusID = Source.FieldVisitStatusID
WHEN MATCHED THEN
UPDATE SET
	FieldVisitStatusName = Source.FieldVisitStatusName,
	FieldVisitStatusDisplayName = Source.FieldVisitStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
	VALUES (FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;