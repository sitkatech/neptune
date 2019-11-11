Drop view if exists dbo.vGeoServerNeighborhood
GO

Create View dbo.vGeoServerNeighborhood As
Select
	NeighborhoodID,
	OCSurveyNeighborhoodID,
	OCSurveyDownstreamNeighborhoodID,
	DrainID,
	Watershed,
	NeighborhoodGeometry4326 as NeighborhoodGeometry,
	NeighborhoodGeometry.STArea() * 2471054 as Area
from dbo.Neighborhood
GO