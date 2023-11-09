create procedure dbo.pProjectLoadGeneratingUnitMakeValid
as
begin

    update dbo.ProjectLoadGeneratingUnit set ProjectLoadGeneratingUnitGeometry = ProjectLoadGeneratingUnitGeometry.MakeValid() where ProjectLoadGeneratingUnitGeometry.STIsValid() = 0
end

GO