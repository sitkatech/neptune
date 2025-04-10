Create View dbo.vGeoServerWaterQualityManagementPlan
as
Select
	w.WaterQualityManagementPlanID as PrimaryKey,
	w.WaterQualityManagementPlanID,
	wqmpb.Geometry4326 as WaterQualityManagementPlanGeometry,
	w.StormwaterJurisdictionID,
	som.OrganizationName as OrganizationName,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when w.TrashCaptureEffectiveness is Null then 0
		else w.TrashCaptureEffectiveness
	end, 0.0) as TrashCaptureEffectiveness,
	tcs.TrashCaptureStatusTypeDisplayName,
	tcs.TrashCaptureStatusTypeID,
	CONCAT('#',tcs.TrashCaptureStatusTypeColorCode) as TrashCaptureStatusTypeColorCode
From
	dbo.WaterQualityManagementPlan w
    left join dbo.WaterQualityManagementPlanBoundary wqmpb on w.WaterQualityManagementPlanID = wqmpb.WaterQualityManagementPlanID
	join dbo.TrashCaptureStatusType tcs on w.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
	join dbo.vStormwaterJurisdictionOrganizationMapping som on w.StormwaterJurisdictionID = som.StormwaterJurisdictionID

GO
