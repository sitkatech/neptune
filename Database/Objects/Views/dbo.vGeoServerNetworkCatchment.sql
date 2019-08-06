Drop view if exists dbo.vGeoServerNetworkCatchment

Create View dbo.vGeoServerNetworkCatchment As
Select
	NetworkCatchmentID,
	OCSurveyCatchmentID,
	OCSurveyDownstreamCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry,
	CatchmentGeometry.STArea() * 2471054 as Area
from dbo.NetworkCatchment
GO