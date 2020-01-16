drop procedure if exists dbo.pLSPCBasinUpdateFromStaging
GO

Create Procedure dbo.pLSPCBasinUpdateFromStaging
As

Merge dbo.LSPCBasin t Using dbo.LSPCBasinStaging s
	on (t.LSPCBasinKey = s.LSPCBasinKey)
When Matched
	Then Update set
		t.LSPCBasinName = s.LSPCBasinName,
		t.LSPCBasinGeometry = s.LSPCBasinGeometry,
		t.LastUpdate = GetDate()
When not matched by Target
	Then insert (
		LSPCBasinKey,
		LSPCBasinName,
		LSPCBasinGeometry,
		LastUpdate
	)
	values (
		s.LSPCBasinKey,
		s.LSPCBasinName,
		s.LSPCBasinGeometry,
		GetDate()
	)
When Not Matched by Source
	Then Delete;
GO