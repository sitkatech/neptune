Create Procedure dbo.pPrecipitationZoneUpdateFromStaging
As

Merge dbo.PrecipitationZone t Using dbo.PrecipitationZoneStaging s
	on (t.PrecipitationZoneKey = s.PrecipitationZoneKey)
When Matched
	Then Update set
		t.DesignStormwaterDepthInInches = s.DesignStormwaterDepthInInches,
		t.PrecipitationZoneGeometry = s.PrecipitationZoneGeometry,
		t.LastUpdate = GetDate()
When not matched by Target
	Then insert (
		PrecipitationZoneKey,
		DesignStormwaterDepthInInches,
		PrecipitationZoneGeometry,
		LastUpdate
	)
	values (
		s.PrecipitationZoneKey,
		s.DesignStormwaterDepthInInches,
		s.PrecipitationZoneGeometry,
		GetDate()
	)
When Not Matched by Source
	Then Delete;
GO