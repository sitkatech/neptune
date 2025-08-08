create procedure dbo.pLoadGeneratingUnitRefreshAreaMakeValid
    @LoadGeneratingUnitRefreshAreaID INT
as
begin

update dbo.LoadGeneratingUnitRefreshArea set LoadGeneratingUnitRefreshAreaGeometry = LoadGeneratingUnitRefreshAreaGeometry.MakeValid() where LoadGeneratingUnitRefreshAreaID = @LoadGeneratingUnitRefreshAreaID
end

GO