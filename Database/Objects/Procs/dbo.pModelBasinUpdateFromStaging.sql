drop procedure if exists dbo.pModelBasinUpdateFromStaging
GO

Create Procedure dbo.pModelBasinUpdateFromStaging
As

Merge dbo.ModelBasin t Using dbo.ModelBasinStaging s
	on (t.ModelBasinKey = s.ModelBasinKey)
When Matched
	Then Update set
		t.ModelBasinName = s.ModelBasinName,
		t.ModelBasinGeometry = s.ModelBasinGeometry,
		t.LastUpdate = GetDate()
When not matched by Target
	Then insert (
		ModelBasinKey,
		ModelBasinName,
		ModelBasinGeometry,
		LastUpdate
	)
	values (
		s.ModelBasinKey,
		s.ModelBasinName,
		s.ModelBasinGeometry,
		GetDate()
	)
When Not Matched by Source
	Then Delete;
GO