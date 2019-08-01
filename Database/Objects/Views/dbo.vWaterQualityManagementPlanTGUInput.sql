Drop view if exists dbo.vWaterQualityManagementPlanTGUInput
Go

Create View dbo.vWaterQualityManagementPlanTGUInput
as
Select
	wp.WaterQualityManagementPlanParcelID as PrimaryKey,
	w.WaterQualityManagementPlanID,
	p.ParcelGeometry,
	w.StormwaterJurisdictionID,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when w.TrashCaptureEffectiveness is Null then 0
		else w.TrashCaptureEffectiveness
	end, 0.0) as TrashCaptureEffectiveness,
	tcs.TrashCaptureStatusTypeDisplayName
From
	dbo.WaterQualityManagementPlan w join dbo.WaterQualityManagementPlanParcel wp
		on w.WaterQualityManagementPlanID = wp.WaterQualityManagementPlanID
	join dbo.Parcel p
		on wp.ParcelID = p.ParcelID
	join dbo.TrashCaptureStatusType tcs
		on w.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
