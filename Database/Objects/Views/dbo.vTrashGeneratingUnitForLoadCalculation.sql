Drop view if exists dbo.vTrashGeneratingUnitLoadBasedFullCapture
GO

Create View dbo.vTrashGeneratingUnitLoadBasedFullCapture
as 
Select
	TrashGeneratingUnitID as PrimaryKey,
	*
from TrashGeneratingUnit
GO