Drop View If Exists dbo.vGeoServerDelineation
GO

Create View dbo.vGeoServerDelineation as
Select
	2 * DelineationID - 1 as PrimaryKey,
	d.DelineationID,
	Null as WaterQualityManagementPlanID,
	DelineationGeometry4326 as DelineationGeometry,
	DelineationTypeName as DelineationType,
	t.TreatmentBMPID,
	sj.StormwaterJurisdictionID,
	t.TreatmentBMPName,
	o.OrganizationName,
	Case
		when d.IsVerified = 1 then 'Verified'
		else 'Provisional'
	end as DelineationStatus,
    tbt.IsAnalyzedInModelingModule
from
	dbo.Delineation d join dbo.DelineationType dt on d.DelineationTypeID = dt.DelineationTypeID
	join dbo.TreatmentBMP t on d.TreatmentBMPID = t.TreatmentBMPID
    join dbo.TreatmentBMPType tbt on t.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
	left join dbo.StormwaterJurisdiction sj on t.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o on sj.OrganizationID = o.OrganizationID
	where t.ProjectID is null

union all

select 
	2 * WaterQualityManagementPlanID as PrimaryKey,
	Null as DelineationID,
	WaterQualityManagementPlanID,
	WaterQualityManagementPlanGeometry as DelineationGeometry,
	'WQMP' as DelineationType,
	Null as TreatmentBMPID,
	StormwaterJurisdictionID,
	Null as TreatmentBMPName,
	OrganizationName,
	'Provisional' as DelineationStatus,
    cast(1 as bit) as IsAnalyzedInModelingModule
from dbo.vGeoServerWaterQualityManagementPlan