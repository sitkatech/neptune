MERGE INTO dbo.OnlandVisualTrashAssessmentScore AS Target
USING (VALUES
(1, 'A', 'A', 4, 2.5),
(2, 'B', 'B', 3, 7.5),
(3, 'C', 'C', 2, 30),
(4, 'D', 'D', 1, 100)
)
AS Source (OnlandVisualTrashAssessmentScoreID, OnlandVisualTrashAssessmentScoreName, OnlandVisualTrashAssessmentScoreDisplayName, NumericValue, TrashGenerationRate)
ON Target.OnlandVisualTrashAssessmentScoreID = Source.OnlandVisualTrashAssessmentScoreID
WHEN MATCHED THEN
UPDATE SET
	OnlandVisualTrashAssessmentScoreName = Source.OnlandVisualTrashAssessmentScoreName,
	OnlandVisualTrashAssessmentScoreDisplayName = Source.OnlandVisualTrashAssessmentScoreDisplayName,
	NumericValue = Source.NumericValue,
	TrashGenerationRate = Source.TrashGenerationRate
WHEN NOT MATCHED BY TARGET THEN
	INSERT (OnlandVisualTrashAssessmentScoreID, OnlandVisualTrashAssessmentScoreName, OnlandVisualTrashAssessmentScoreDisplayName, NumericValue, TrashGenerationRate)
	VALUES (OnlandVisualTrashAssessmentScoreID, OnlandVisualTrashAssessmentScoreName, OnlandVisualTrashAssessmentScoreDisplayName, NumericValue, TrashGenerationRate)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;