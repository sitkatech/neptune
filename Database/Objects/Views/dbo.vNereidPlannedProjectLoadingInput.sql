drop view if exists dbo.vNereidPlannedProjectLoadingInput
GO

create view dbo.vNereidPlannedProjectLoadingInput
as
select
	hru.PlannedProjectHRUCharacteristicID as PrimaryKey,
	hru.ProjectID,
	d.DelineationID,
	lgu.WaterQualityManagementPlanID,
	rsb.RegionalSubbasinID, -- don't actually need this but it felt silly to leave it off
	OCSurveyCatchmentID,
	ModelBasinKey,
	hrucode.HRUCharacteristicLandUseCodeName as LandUseCode,
	basehrucode.HRUCharacteristicLandUseCodeName as BaselineLandUseCode,
	HydrologicSoilGroup,
	SlopePercentage,
	Area,
	ImperviousAcres,
	BaselineImperviousAcres,
	d.IsVerified as DelineationIsVerified,
	wqmp.WaterQualityManagementPlanModelingApproachID as SpatiallyAssociatedModelingApproach,
	bwqmp.WaterQualityManagementPlanModelingApproachID as RelationallyAssociatedModelingApproach
from
	dbo.PlannedProjectHRUCharacteristic hru join dbo.PlannedProjectLoadGeneratingUnit lgu
		on hru.PlannedProjectLoadGeneratingUnitID = lgu.PlannedProjectLoadGeneratingUnitID
	join dbo.RegionalSubbasin rsb
		on lgu.RegionalSubbasinID = rsb.RegionalSubbasinID
	join dbo.ModelBasin Model
		on lgu.ModelBasinID = Model.ModelBasinID
	join dbo.HRUCharacteristicLandUseCode hrucode
		on hru.HRUCharacteristicLandUseCodeID = hrucode.HRUCharacteristicLandUseCodeID
	join dbo.HRUCharacteristicLandUseCode basehrucode
		on hru.BaselineHRUCharacteristicLandUseCodeID = basehrucode.HRUCharacteristicLandUseCodeID
	left join dbo.Delineation d
		on d.DelineationID = lgu.DelineationID
	left join dbo.WaterQualityManagementPlan wqmp
		on lgu.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
	left join dbo.TreatmentBMP bmp
		on d.TreatmentBMPID = bmp.TreatmentBMPID
	left join dbo.WaterQualityManagementPlan bwqmp
		on bmp.WaterQualityManagementPlanID = bwqmp.WaterQualityManagementPlanID
  where
	hru.Area > 0
GO

