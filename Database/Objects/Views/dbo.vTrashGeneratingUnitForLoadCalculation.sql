Drop view if exists dbo.vTrashGeneratingUnitForLoadCalculation
GO

Create View dbo.vTrashGeneratingUnitForLoadCalculation
as 
Select
	TrashGeneratingUnitID as PrimaryKey,
	*
from TrashGeneratingUnit
GO