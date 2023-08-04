MERGE INTO dbo.TreatmentBMPLifespanType AS Target
USING (VALUES
(1, 'Unspecified', 'Unspecified/Voluntary'),
(2, 'Perpetuity', 'Perpetuity/Life of Project'),
(3, 'FixedEndDate', 'Fixed End Date')
)
AS Source (TreatmentBMPLifespanTypeID, TreatmentBMPLifespanTypeName, TreatmentBMPLifespanTypeDisplayName)
ON Target.TreatmentBMPLifespanTypeID = Source.TreatmentBMPLifespanTypeID
WHEN MATCHED THEN
UPDATE SET
	TreatmentBMPLifespanTypeName = Source.TreatmentBMPLifespanTypeName,
	TreatmentBMPLifespanTypeDisplayName = Source.TreatmentBMPLifespanTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (TreatmentBMPLifespanTypeID, TreatmentBMPLifespanTypeName, TreatmentBMPLifespanTypeDisplayName)
	VALUES (TreatmentBMPLifespanTypeID, TreatmentBMPLifespanTypeName, TreatmentBMPLifespanTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;