Drop View If Exists dbo.vLSPCBasinLGUInput
GO

Create view dbo.vLSPCBasinLGUInput
as
Select
	LSPCBasinID,
	LSPCBasinGeometry
From dbo.LSPCBasin
GO