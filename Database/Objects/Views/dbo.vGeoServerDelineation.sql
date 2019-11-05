Drop View If Exists dbo.vGeoServerDelineation
GO

Drop view if exists dbo.vGeoServerWaterQualityManagementPlan
Go

Create View dbo.vGeoServerWaterQualityManagementPlan
as
Select
	w.WaterQualityManagementPlanID as PrimaryKey,
	w.WaterQualityManagementPlanID,
	WaterQualityManagementPlanGeometry,
	w.StormwaterJurisdictionID,
	som.OrganizationName as OrganizationName,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when w.TrashCaptureEffectiveness is Null then 0
		else w.TrashCaptureEffectiveness
	end, 0.0) as TrashCaptureEffectiveness,
	tcs.TrashCaptureStatusTypeDisplayName
From
	dbo.WaterQualityManagementPlan w join (
		Select 
			wp.WaterQualityManagementPlanID,
			geometry::UnionAggregate(ParcelGeometry4326) as WaterQualityManagementPlanGeometry
		from WaterQualityManagementPlanParcel wp join Parcel p
			on wp.ParcelID = p.ParcelID
		group by wp.WaterQualityManagementPlanID
	) wp
		on w.WaterQualityManagementPlanID = wp.WaterQualityManagementPlanID
	join dbo.TrashCaptureStatusType tcs
		on w.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
	join dbo.vStormwaterJurisdictionOrganizationMapping som
		on w.StormwaterJurisdictionID = som.StormwaterJurisdictionID

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
	2 * WaterQualityManagementPlanID as PrimaryKey,
	Null as DelineationID,
	WaterQualityManagementPlanID,
	WaterQualityManagementPlanGeometry as DelineationGeometry,
	'WQMP' as DelineationType,
	Null as TreatmentBMPID,
	StormwaterJurisdictionID,
	Null as TreatmentBMPName,
	OrganizationName,
	'Provisional' as DelineationStatus
from vGeoServerWaterQualityManagementPlan