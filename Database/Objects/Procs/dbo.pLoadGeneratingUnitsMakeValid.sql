drop procedure if exists dbo.pLoadGeneratingUnitsMakeValid
GO

Create Procedure dbo.pLoadGeneratingUnitsMakeValid

As
update dbo.LoadGeneratingUnit 
set LoadGeneratingUnitGeometry = LoadGeneratingUnitGeometry.MakeValid() 
where LoadGeneratingUnitGeometry.STIsValid() = 0

GO