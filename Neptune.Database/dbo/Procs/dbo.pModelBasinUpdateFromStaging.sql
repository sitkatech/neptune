Create Procedure dbo.pModelBasinUpdateFromStaging
As

Merge dbo.ModelBasin t Using dbo.ModelBasinStaging s
	on (t.ModelBasinKey = s.ModelBasinKey)
When Matched
	Then Update set
		t.ModelBasinRegion = s.ModelBasinRegion,
		t.ModelBasinState = s.ModelBasinState,
		t.ModelBasinGeometry = s.ModelBasinGeometry,
		t.LastUpdate = GetDate()
When not matched by Target
	Then insert (
		ModelBasinKey,
		ModelBasinGeometry,
		LastUpdate,
		ModelBasinState,
		ModelBasinRegion
	)
	values (
		s.ModelBasinKey,
		s.ModelBasinGeometry,
		GetDate(),
		ModelBasinState,
		ModelBasinRegion
	)
When Not Matched by Source
	Then Delete;
GO