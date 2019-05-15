Alter Table dbo.OnlandVisualTrashAssessmentScore 
Add TrashGeneratingRate Decimal Null

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGeneratingRate = 2.5 Where NumericValue = 4

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGeneratingRate = 7.5 Where NumericValue = 3

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGeneratingRate = 30 Where NumericValue = 2

Go

Update dbo.OnlandVisualTrashAssessmentScore Set TrashGeneratingRate = 100 Where NumericValue = 1

Go

Alter Table dbo.OnlandVisualTrashAssessmentScore 
Alter Column TrashGeneratingRate Decimal Not Null