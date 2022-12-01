Drop view if exists dbo.vPyQgisWaterQualityManagementPlanTGUInput
Go

Create View dbo.vPyQgisWaterQualityManagementPlanTGUInput
as
Select
	w.WaterQualityManagementPlanID as WQMPID,
	w.WaterQualityManagementPlanBoundary,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when w.TrashCaptureEffectiveness is Null then 0
		else w.TrashCaptureEffectiveness
	end, 0.0) as TCEffect
From
	dbo.WaterQualityManagementPlan w 
	join dbo.TrashCaptureStatusType tcs
		on w.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
	join dbo.vStormwaterJurisdictionOrganizationMapping som
		on w.StormwaterJurisdictionID = som.StormwaterJurisdictionID
where w.WaterQualityManagementPlanBoundary is not null

GO
