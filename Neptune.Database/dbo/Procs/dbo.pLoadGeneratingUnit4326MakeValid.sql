create procedure dbo.pLoadGeneratingUnit4326MakeValid
as
begin

    update dbo.LoadGeneratingUnit4326 set LoadGeneratingUnit4326Geometry = LoadGeneratingUnit4326Geometry.MakeValid() where LoadGeneratingUnit4326Geometry.STIsValid() = 0
end

GO