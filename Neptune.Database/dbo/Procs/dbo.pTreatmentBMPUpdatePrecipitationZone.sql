drop procedure if exists dbo.pTreatmentBMPUpdatePrecipitationZone
GO

Create Procedure dbo.pTreatmentBMPUpdatePrecipitationZone
As

update t
set t.PrecipitationZoneID = l.PrecipitationZoneID
from dbo.TreatmentBMP t
left join dbo.PrecipitationZone l on t.LocationPoint.STIntersects(l.PrecipitationZoneGeometry) = 1


GO