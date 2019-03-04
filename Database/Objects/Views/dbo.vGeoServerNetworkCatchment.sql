Create View dbo.vGeoServerNetworkCatchment As
Select
	NetworkCatchmentID,
	OCSurveyCatchmentIDN,
	OCSurveyDownstreamCatchmentIDN,
	DrainID,
	Watershed,
	CatchmentGeometry
From dbo.NetworkCatchment
GO