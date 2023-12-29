MERGE INTO dbo.OnlandVisualTrashAssessmentStatus AS Target
USING (VALUES
(1, 'InProgress', 'In Progress'),
(2, 'Complete', 'Complete')
)
AS Source (OnlandVisualTrashAssessmentStatusID, OnlandVisualTrashAssessmentStatusName, OnlandVisualTrashAssessmentStatusDisplayName)
ON Target.OnlandVisualTrashAssessmentStatusID = Source.OnlandVisualTrashAssessmentStatusID
WHEN MATCHED THEN
UPDATE SET
	OnlandVisualTrashAssessmentStatusName = Source.OnlandVisualTrashAssessmentStatusName,
	OnlandVisualTrashAssessmentStatusDisplayName = Source.OnlandVisualTrashAssessmentStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (OnlandVisualTrashAssessmentStatusID, OnlandVisualTrashAssessmentStatusName, OnlandVisualTrashAssessmentStatusDisplayName)
	VALUES (OnlandVisualTrashAssessmentStatusID, OnlandVisualTrashAssessmentStatusName, OnlandVisualTrashAssessmentStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;