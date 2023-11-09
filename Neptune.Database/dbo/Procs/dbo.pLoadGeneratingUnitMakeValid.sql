create procedure dbo.pLoadGeneratingUnitMakeValid
as
begin

    update dbo.LoadGeneratingUnit set LoadGeneratingUnitGeometry = LoadGeneratingUnitGeometry.MakeValid() where LoadGeneratingUnitGeometry.STIsValid() = 0
end

GO