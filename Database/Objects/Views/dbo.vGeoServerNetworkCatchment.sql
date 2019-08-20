Drop view if exists dbo.vGeoServerNetworkCatchment
GO

Create View dbo.vGeoServerNetworkCatchment As
Select
	NetworkCatchmentID,
	OCSurveyCatchmentID,
	OCSurveyDownstreamCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry4326 as CatchmentGeometry,
	CatchmentGeometry.STArea() * 2471054 as Area
from dbo.NetworkCatchment
GO