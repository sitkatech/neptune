drop procedure if exists dbo.pTreatmentBMPUpdateModelBasin
GO

Create Procedure dbo.pTreatmentBMPUpdateModelBasin
As

update t
set t.ModelBasinID = l.ModelBasinID
from dbo.TreatmentBMP t
left join dbo.ModelBasin l on t.LocationPoint.STIntersects(l.ModelBasinGeometry) = 1


GO