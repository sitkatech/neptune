Drop View If Exists dbo.vGeoServerBackbone
Go

Create View dbo.vGeoServerBackbone
as
Select
	BackboneSegmentID,
CatchIDN,
NeighborhoodID,
b. BackboneSegmentTypeID,
DownstreamBackboneSegmentID,
StreamName,
BackboneSegmentGeometry4326 as BackboneSegmentGeometry,
	t.BackboneSegmentTypeName as BackboneSegmentType
From
	dbo.BackboneSegment b join dbo.BackboneSegmentType t
		on b.BackboneSegmentTypeID = t.BackboneSegmentTypeID

