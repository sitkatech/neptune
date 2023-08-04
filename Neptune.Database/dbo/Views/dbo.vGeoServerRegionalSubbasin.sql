Create View dbo.vGeoServerRegionalSubbasin As
Select
	RegionalSubbasinID,
	OCSurveyCatchmentID,
	OCSurveyDownstreamCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry4326 as CatchmentGeometry,
	CatchmentGeometry.STArea() * 2471054 as Area
from dbo.RegionalSubbasin
GO