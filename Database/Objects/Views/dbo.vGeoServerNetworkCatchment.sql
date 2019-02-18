Create View dbo.vGeoServerNetworkCatchment As
Select
	NetworkCatchmentID,
	OCSurveyCatchmentID,
	DownstreamCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry
From dbo.NetworkCatchment
GO