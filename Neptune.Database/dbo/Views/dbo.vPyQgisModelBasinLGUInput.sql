Create view dbo.vPyQgisModelBasinLGUInput
as
Select
	ModelBasinID as ModelID,
	ModelBasinGeometry
From dbo.ModelBasin
GO