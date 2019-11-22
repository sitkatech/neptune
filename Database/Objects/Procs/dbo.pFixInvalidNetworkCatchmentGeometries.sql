drop procedure if exists dbo.pFixInvalidNetworkCatchmentGeometries
GO

Create Procedure dbo.pFixInvalidNetworkCatchmentGeometries
As

Update NetworkCatchment
set CatchmentGeometry4326 = CatchmentGeometry4326.MakeValid()
where CatchmentGeometry4326.STIsValid() = 0

Update NetworkCatchment
set CatchmentGeometry = CatchmentGeometry.MakeValid()
where CatchmentGeometry.STIsValid() = 0
