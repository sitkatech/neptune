drop procedure if exists dbo.pTreatmentBMPUpdateWatershed
GO

Create Procedure dbo.pTreatmentBMPUpdateWatershed
As

update t
set t.WatershedID = l.WatershedID
from dbo.TreatmentBMP t
left join dbo.Watershed l on t.LocationPoint.STIntersects(l.WatershedGeometry) = 1


GO