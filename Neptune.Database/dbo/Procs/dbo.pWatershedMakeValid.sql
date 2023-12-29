create procedure dbo.pWatershedMakeValid
as
begin

    update dbo.Watershed set WatershedGeometry = WatershedGeometry.MakeValid() where WatershedGeometry.STIsValid() = 0
    update dbo.Watershed set WatershedGeometry4326 = WatershedGeometry4326.MakeValid() where WatershedGeometry4326.STIsValid() = 0
    update dbo.RegionalSubbasin set CatchmentGeometry = CatchmentGeometry.MakeValid() where CatchmentGeometry.STIsValid() = 0
    update dbo.RegionalSubbasin set CatchmentGeometry4326 = CatchmentGeometry4326.MakeValid() where CatchmentGeometry4326.STIsValid() = 0
end

GO