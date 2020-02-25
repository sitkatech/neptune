Drop View If Exists dbo.vGeoServerMaskLayer
go

Create View dbo.vGeoServerMaskLayer as

Select geometry::UnionAggregate(Geometry4326).STEnvelope().STBuffer(1).STDifference(geometry::UnionAggregate(Geometry4326)) as MaskLayerGeometry from dbo.StormwaterJurisdictionGeometry
GO