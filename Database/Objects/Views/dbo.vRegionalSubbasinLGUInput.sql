Drop View If Exists dbo.vRegionalSubbasinLGUInput
GO

Create view dbo.vRegionalSubbasinLGUInput
as
Select
	RegionalSubbasinID,
	CatchmentGeometry
From dbo.RegionalSubbasin
GO