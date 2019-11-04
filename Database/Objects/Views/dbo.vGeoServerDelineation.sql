Drop View If Exists dbo.vGeoServerDelineation
GO

Create View dbo.vGeoServerDelineation as
Select
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
	end as DelineationStatus
from
	dbo.Delineation d inner join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	inner join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	left join dbo.StormwaterJurisdiction sj
		on t.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on sj.OrganizationID = o.OrganizationID

union all

select 
	Null as DelineationID,
	WaterQualityManagementPlanID,
	WaterQualityManagementPlanGeometry as DelineationGeometry,
	'WQMP' as DelineationType,
	Null as TreatmentBMPID,
	StormwaterJurisdictionID,
	Null as TreatmentBMPName,
	OrganizationName,
	'Provisional' as DelineationStatus
from vWaterQualityManagementPlanTGUInput