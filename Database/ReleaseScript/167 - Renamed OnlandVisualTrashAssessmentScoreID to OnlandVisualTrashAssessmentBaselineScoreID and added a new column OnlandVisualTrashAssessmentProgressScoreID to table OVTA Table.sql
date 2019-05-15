exec sp_rename 'dbo.FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID', 'FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash', 'OBJECT';
exec sp_rename 'dbo.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScoreID', 'OnlandVisualTrashAssessmentBaselineScoreID', 'COLUMN';


Alter Table dbo.OnlandVisualTrashAssessmentArea
Add OnlandVisualTrashAssessmentProgressScoreID Int Null
	CONSTRAINT FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash
	FOREIGN KEY REFERENCES dbo.OnlandVisualTrashAssessmentScore (OnlandVisualTrashAssessmentScoreID)
