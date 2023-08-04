MERGE INTO dbo.PermitType AS Target
USING (VALUES
(1, 'PhaseIMS4', 'Phase I MS4'),
(2, 'PhaseIIMS4', 'Phase II MS4'),
(3, 'IGP', 'IGP'),
(4, 'IndividualPermit', 'Individual Permit'),
(5, 'CalTransMS4', 'CalTrans MS4'),
(6, 'Other', 'Other')
)
AS Source (PermitTypeID, PermitTypeName, PermitTypeDisplayName)
ON Target.PermitTypeID = Source.PermitTypeID
WHEN MATCHED THEN
UPDATE SET
	PermitTypeName = Source.PermitTypeName,
	PermitTypeDisplayName = Source.PermitTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (PermitTypeID, PermitTypeName, PermitTypeDisplayName)
	VALUES (PermitTypeID, PermitTypeName, PermitTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;