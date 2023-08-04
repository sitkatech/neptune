MERGE INTO dbo.TreatmentBMPAssessmentType AS Target
USING (VALUES
(1, 'Initial', 'Initial'),
(2, 'PostMaintenance', 'Post-Maintenance')
)
AS Source (TreatmentBMPAssessmentTypeID, TreatmentBMPAssessmentTypeName, TreatmentBMPAssessmentTypeDisplayName)
ON Target.TreatmentBMPAssessmentTypeID = Source.TreatmentBMPAssessmentTypeID
WHEN MATCHED THEN
UPDATE SET
	TreatmentBMPAssessmentTypeName = Source.TreatmentBMPAssessmentTypeName,
	TreatmentBMPAssessmentTypeDisplayName = Source.TreatmentBMPAssessmentTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (TreatmentBMPAssessmentTypeID, TreatmentBMPAssessmentTypeName, TreatmentBMPAssessmentTypeDisplayName)
	VALUES (TreatmentBMPAssessmentTypeID, TreatmentBMPAssessmentTypeName, TreatmentBMPAssessmentTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
