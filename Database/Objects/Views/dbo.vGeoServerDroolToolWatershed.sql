Drop View If Exists dbo.vGeoServerDroolToolWatershed
Go

Create View dbo.vGeoServerDroolToolWatershed
as
Select
	DroolToolWatershedID,
	DroolToolWatershedName,
	DroolToolWatershedGeometry4326 as DroolToolWatershedGeometry
From
	dbo.DroolToolWatershed