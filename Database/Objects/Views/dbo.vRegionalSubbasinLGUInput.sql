Drop View If Exists dbo.vRegionalSubbasinLGUInput
GO

Create view dbo.vRegionalSubbasinLGUInput
as
Select
	RegionalSubbasinID as RSBID,
	CatchmentGeometry
From dbo.RegionalSubbasin
GO