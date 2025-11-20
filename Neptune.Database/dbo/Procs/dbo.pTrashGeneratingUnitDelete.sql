Create Procedure dbo.pTrashGeneratingUnitDelete
with execute as owner
As

TRUNCATE TABLE dbo.TrashGeneratingUnit4326
ALTER TABLE dbo.TrashGeneratingUnit4326 DROP CONSTRAINT FK_TrashGeneratingUnit4326_TrashGeneratingUnit_TrashGeneratingUnitID;

TRUNCATE TABLE dbo.TrashGeneratingUnit

ALTER TABLE dbo.TrashGeneratingUnit4326 ADD CONSTRAINT [FK_TrashGeneratingUnit4326_TrashGeneratingUnit_TrashGeneratingUnitID] FOREIGN KEY ([TrashGeneratingUnitID]) REFERENCES [dbo].[TrashGeneratingUnit] ([TrashGeneratingUnitID])

GO