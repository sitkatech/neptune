create procedure dbo.pLoadGeneratingUnitMakeValid
as
begin

    update dbo.LoadGeneratingUnit set LoadGeneratingUnitGeometry = LoadGeneratingUnitGeometry.MakeValid() where LoadGeneratingUnitGeometry.STIsValid() = 0
    update dbo.LoadGeneratingUnit set LoadGeneratingUnitGeometry4326 = LoadGeneratingUnitGeometry4326.MakeValid() where LoadGeneratingUnitGeometry4326.STIsValid() = 0
end

GO