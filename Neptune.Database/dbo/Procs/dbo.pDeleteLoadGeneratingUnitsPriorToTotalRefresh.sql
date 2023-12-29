Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh 
As
Delete from dbo.HRUCharacteristic
Delete from dbo.LoadGeneratingUnit
GO
