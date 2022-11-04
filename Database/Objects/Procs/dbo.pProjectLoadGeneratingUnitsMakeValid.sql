drop procedure if exists dbo.pProjectLoadGeneratingUnitsMakeValid
GO

Create Procedure dbo.pProjectLoadGeneratingUnitsMakeValid

As
update dbo.ProjectLoadGeneratingUnit 
set ProjectLoadGeneratingUnitGeometry = ProjectLoadGeneratingUnitGeometry.MakeValid() 
where ProjectLoadGeneratingUnitGeometry.STIsValid() = 0

GO