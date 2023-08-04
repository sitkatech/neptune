-- this view gives, for each WQMP:
-- -its ID,
-- -a bit telling whether the LGUs are populated, (LoadGeneratingUnitsPopulated)
-- -a bit telling whether the Boundary is defined, (BoundaryIsDefined)
-- -and an int telling how mnay Model basins it intersects (CountOfIntersectingModelBasins)
-- The upshot is that LoadGeneratingUnitsPopulated should = BoundaryIsDefined AND CountOfIntersectingModelBasins

drop view if exists dbo.vWaterQualityManagementPlanLGUAudit
GO

create view dbo.vWaterQualityManagementPlanLGUAudit
as
select 
	wqmp.WaterQualityManagementPlanID as PrimaryKey,
    wqmp.WaterQualityManagementPlanID as WaterQualityManagementPlanID,
	wqmp.WaterQualityManagementPlanName,
	wqmpb.GeometryNative as WaterQualityManagementPlanBoundary,
	cast(case
		when CountOfLGUs is not null then 1
		else 0
	end as bit) as LoadGeneratingUnitsPopulated,
	cast(case
		when wqmpb.GeometryNative is not null then 1
		else 0
	end as bit) as BoundaryIsDefined,
	IsNull(ModelCount.CountOfModels, 0) as CountOfIntersectingModelBasins
from dbo.WaterQualityManagementPlan wqmp
left join dbo.WaterQualityManagementPlanBoundary wqmpb on wqmp.WaterQualityManagementPlanID = wqmpb.WaterQualityManagementPlanID
	left join (
		select
			count(*) as CountOfLGUs,
			WaterQualityManagementPlanID
		from dbo.LoadGeneratingUnit
		group by WaterQualityManagementPlanID
	) lgucount on lgucount.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
	left join (
		select
			WaterQualityManagementPlanID,
			count(*) as CountOfModels
		from dbo.WaterQualityManagementPlanBoundary wqmpb
			left join dbo.ModelBasin Model
				on Model.ModelBasinGeometry.STIntersects(wqmpb.GeometryNative)  = 1
		where ModelBasinID is not null
		group by WaterQualityManagementPlanID
	) ModelCount on wqmp.WaterQualityManagementPlanID = ModelCount.WaterQualityManagementPlanID

go