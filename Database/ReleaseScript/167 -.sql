--begin tran

----drop constraint that depends on old column name
--ALTER TABLE dbo.OnlandVisualTrashAssessment DROP CONSTRAINT FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID

--GO

--EXEC sp_rename 'OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID', 'OnlandVisualTrashBaselineScoreID', 'COLUMN';

--GO

---- recreate the constraint depending on new column name (todo: change name)
--ALTER TABLE dbo.OnlandVisualTrashAssessment  WITH CHECK ADD  CONSTRAINT FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash FOREIGN KEY(OnlandVisualTrashAssessmentBaselineScoreID)
--REFERENCES dbo.OnlandVisualTrashAssessmentScore (OnlandVisualTrashAssessmentScoreID)

--GO

--Alter Table dbo.OnlandVisualTrashAssessment 
--Add OnlandVisualTrashAssessmentProgressScoreID Int Null Constraint FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash FOREIGN KEY(OnlandVisualTrashAssessmentProgressScoreID)
--REFERENCES dbo.OnlandVisualTrashAssessmentScore (OnlandVisualTrashAssessmentScoreID)

--rollback