create view  dbo.vGeoServerLoadGeneratingUnit
as
select
	LoadGeneratingUnitID,
	[LoadGeneratingUnitGeometry4326],
	[ModelBasinID],
    [RegionalSubbasinID],
	[DelineationID],
	[WaterQualityManagementPlanID],
	[IsEmptyResponseFromHRUService],
	[DateHRURequested]
from dbo.LoadGeneratingUnit
GO
