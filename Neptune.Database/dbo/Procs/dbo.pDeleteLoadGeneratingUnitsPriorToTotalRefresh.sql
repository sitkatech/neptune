Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh 
with execute as owner
as
truncate table dbo.HRUCharacteristic
Delete from dbo.LoadGeneratingUnit
DBCC CHECKIDENT ('dbo.LoadGeneratingUnit', RESEED, 0);

GO
