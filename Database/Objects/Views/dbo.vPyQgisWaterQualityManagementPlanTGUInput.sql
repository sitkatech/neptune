Drop view if exists dbo.vPyQgisWaterQualityManagementPlanTGUInput
Go

Create View dbo.vPyQgisWaterQualityManagementPlanTGUInput
as
Select
	wqmp.WaterQualityManagementPlanID as WQMPID,
	wqmpb.GeometryNative as WaterQualityManagementPlanBoundary,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when wqmp.TrashCaptureEffectiveness is Null then 0
		else wqmp.TrashCaptureEffectiveness
	end, 0.0) as TCEffect
from dbo.WaterQualityManagementPlanBoundary wqmpb
join dbo.WaterQualityManagementPlan wqmp on wqmpb.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
join dbo.TrashCaptureStatusType tcs on wqmp.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
join dbo.vStormwaterJurisdictionOrganizationMapping som on wqmp.StormwaterJurisdictionID = som.StormwaterJurisdictionID
where wqmpb.GeometryNative is not null

GO
