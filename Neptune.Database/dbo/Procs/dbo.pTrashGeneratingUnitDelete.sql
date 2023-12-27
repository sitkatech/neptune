Create Procedure dbo.pTrashGeneratingUnitDelete
with execute as owner
As

TRUNCATE TABLE dbo.TrashGeneratingUnit
TRUNCATE TABLE dbo.TrashGeneratingUnit4326

GO