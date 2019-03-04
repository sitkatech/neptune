Create View dbo.vGeoServerNetworkCatchment As
Select
	NetworkCatchmentID,
	OCSurveyCatchmentID,
	OCSurveyDownstreamCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry
From dbo.NetworkCatchment
GO