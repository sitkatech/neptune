Drop View If Exists dbo.vGeoServerMaskLayer
go

Create View dbo.vGeoServerMaskLayer as

Select geometry::UnionAggregate(StormwaterJurisdictionGeometry).STEnvelope().STDifference(geometry::UnionAggregate(StormwaterJurisdictionGeometry)) as MaskLayerGeometry from dbo.StormwaterJurisdiction
GO