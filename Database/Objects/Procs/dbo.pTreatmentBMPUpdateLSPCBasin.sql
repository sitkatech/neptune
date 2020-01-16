drop procedure if exists dbo.pTreatmentBMPUpdateLSPCBasin
GO

Create Procedure dbo.pTreatmentBMPUpdateLSPCBasin
As

update t
set t.LSPCBasinID = l.LSPCBasinID
from dbo.TreatmentBMP t
left join dbo.LSPCBasin l on t.LocationPoint.STIntersects(l.LSPCBasinGeometry) = 1


GO