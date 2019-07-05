Drop View If Exists dbo.vGeoServerBackbone
Go

Create View dbo.vGeoServerBackbone
as
Select
	b.*,
	t.BackboneSegmentTypeName as BackboneSegmentType
From
	dbo.BackboneSegment b join dbo.BackboneSegmentType t
		on b.BackboneSegmentTypeID = t.BackboneSegmentTypeID

