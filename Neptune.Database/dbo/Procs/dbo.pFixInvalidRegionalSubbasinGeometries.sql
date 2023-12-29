Create Procedure dbo.pFixInvalidRegionalSubbasinGeometries
As

Update RegionalSubbasin
set CatchmentGeometry4326 = CatchmentGeometry4326.MakeValid()
where CatchmentGeometry4326.STIsValid() = 0

Update RegionalSubbasin
set CatchmentGeometry = CatchmentGeometry.MakeValid()
where CatchmentGeometry.STIsValid() = 0
