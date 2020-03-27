-- this view gives, for each WQMP:
-- -its ID,
-- -a bit telling whether the LGUs are populated, (LoadGeneratingUnitsPopulated)
-- -a bit telling whether the Boundary is defined, (BoundaryIsDefined)
-- -and an int telling how mnay LSPC basins it intersects (CountOfIntersectingLSPCBasins)
-- The upshot is that LoadGeneratingUnitsPopulated should = BoundaryIsDefined AND CountOfIntersectingLSPCBasins

drop view if exists dbo.vWaterQualityManagementPlanLGUAudit
GO

create view dbo.vWaterQualityManagementPlanLGUAudit
as
select 
	wqmp.WaterQualityManagementPlanID as PrimaryKey,
    wqmp.WaterQualityManagementPlanID as WaterQualityManagementPlanID,
	wqmp.WaterQualityManagementPlanName,
	wqmp.WaterQualityManagementPlanBoundary,
	cast(case
		when CountOfLGUs is not null then 1
		else 0
	end as bit) as LoadGeneratingUnitsPopulated,
	cast(case
		when WaterQualityManagementPlanBoundary is not null then 1
		else 0
	end as bit) as BoundaryIsDefined,
	IsNull(lspcCount.CountOfLSPCs, 0) as CountOfIntersectingLSPCBasins
from dbo.WaterQualityManagementPlan wqmp
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
			count(*) as CountOfLSPCs
		from dbo.WaterQualityManagementPlan wqmp
			left join dbo.LSPCBasin lspc
				on lspc.LSPCBasinGeometry.STIntersects(wqmp.WaterQualityManagementPlanBoundary)  = 1
		where LSPCBasinID is not null
		group by WaterQualityManagementPlanID
	) lspcCount on wqmp.WaterQualityManagementPlanID = lspcCount.WaterQualityManagementPlanID

go