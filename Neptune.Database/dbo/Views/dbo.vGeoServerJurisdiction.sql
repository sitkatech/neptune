Create View dbo.vGeoServerJurisdiction As
Select
	s.StormwaterJurisdictionID,
	s.OrganizationID,
	s.StateProvinceID,
	Geometry4326 as StormwaterJurisdictionGeometry
From dbo.StormwaterJurisdiction s
join dbo.StormwaterJurisdictionGeometry sg on s.StormwaterJurisdictionID = sg.StormwaterJurisdictionID