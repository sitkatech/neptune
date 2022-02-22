Drop View If Exists dbo.vModelBasinLGUInput
GO

Create view dbo.vModelBasinLGUInput
as
Select
	ModelBasinID as ModelID,
	ModelBasinGeometry
From dbo.ModelBasin
GO