Drop View If Exists dbo.vGeoServerJurisdiction
Go

Create View dbo.vGeoServerJurisdiction As
Select
	StormwaterJurisdictionID,
OrganizationID,

StateProvinceID,
IsTransportationJurisdiction,
StormwaterJurisdictionGeometry4326 as StormwaterJurisdictionGeometry
From dbo.StormwaterJurisdiction