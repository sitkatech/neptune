Drop View If Exists dbo.vRegionalSubbasinLGUInput
GO

Create view dbo.vRegionalSubbasinLGUInput
as
Select
	RegionalSubbasinID as RSBID,
	CatchmentGeometry,
	ModelBasinID as ModelID -- Must rename fields to have less than 10 characters for compatiblity with shapefile format
From dbo.RegionalSubbasin
GO