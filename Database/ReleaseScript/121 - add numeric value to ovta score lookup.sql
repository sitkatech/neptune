Alter Table dbo.OnlandVisualTrashAssessmentScore
Add NumericValue int null
Go

Update dbo.OnlandVisualTrashAssessmentScore
set NumericValue = OnlandVisualTrashAssessmentScoreID
Go

Alter Table dbo.OnlandVisualTrashAssessmentScore
Alter Column NumericValue int not null
Go