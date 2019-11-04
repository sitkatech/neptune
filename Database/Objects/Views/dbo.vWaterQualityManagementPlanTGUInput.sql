Drop view if exists dbo.vWaterQualityManagementPlanTGUInput
Go

Create View dbo.vWaterQualityManagementPlanTGUInput
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
			geometry::UnionAggregate(ParcelGeometry) as WaterQualityManagementPlanGeometry
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
