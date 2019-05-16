Alter Table dbo.OnlandVisualTrashAssessmentScore 
Add TrashGenerationRate Decimal Null

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGenerationRate = 2.5 Where NumericValue = 4

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGenerationRate = 7.5 Where NumericValue = 3

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGenerationRate = 30 Where NumericValue = 2

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGenerationRate = 100 Where NumericValue = 1

Go

Alter Table dbo.OnlandVisualTrashAssessmentScore 
Alter Column TrashGenerationRate Decimal Not Null