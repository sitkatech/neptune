create view  dbo.vGeoServerLoadGeneratingUnit
as
select
	LoadGeneratingUnit4326ID,
	[LoadGeneratingUnit4326Geometry],
	[ModelBasinID],
    [RegionalSubbasinID],
	[DelineationID],
	[WaterQualityManagementPlanID],
	[IsEmptyResponseFromHRUService],
	[DateHRURequested]
from dbo.LoadGeneratingUnit4326
GO
