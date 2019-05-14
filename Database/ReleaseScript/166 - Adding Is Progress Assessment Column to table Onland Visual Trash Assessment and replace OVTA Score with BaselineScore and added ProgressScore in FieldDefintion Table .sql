Alter Table dbo.OnlandVisualTrashAssessment
Add IsProgressAssessment Bit Null

GO

Update dbo.OnlandVisualTrashAssessment Set IsProgressAssessment = 0

GO

Alter Table dbo.OnlandVisualTrashAssessment
Alter Column IsProgressAssessment Bit Not Null
