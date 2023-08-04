Create view dbo.vPowerBILandUseStatistic
as
select
	hru.HRUCharacteristicID as PrimaryKey,
	hru.HRUCharacteristicID,
	hru.HydrologicSoilGroup,
	hru.SlopePercentage,
	hru.ImperviousAcres,
	hru.Area,
	luc.HRUCharacteristicLandUseCodeDisplayName,
	lgu.ModelBasinID,
	rsb.Watershed as WatershedName,
	rsb.OCSurveyCatchmentID as CatchIDN,
	rsb.OCSurveyDownstreamCatchmentID as DownCatchIDN,
	d.TreatmentBMPID,
	d.DelineationID,
	lgu.WaterQualityManagementPlanID,
	lgu.RegionalSubbasinID,
	lgu.LoadGeneratingUnitID,
	Model.ModelBasinKey,
	luc.HRUCharacteristicLandUseCodeName as LandUse,
	concat(Model.ModelBasinKey, '-', luc.HRUCharacteristicLandUseCodeName, '-', hru.HydrologicSoilGroup, '-', hru.SlopePercentage) as SurfaceKey
from
	dbo.HRUCharacteristic hru left join dbo.LoadGeneratingUnit lgu
		on hru.LoadGeneratingUnitID = lgu.LoadGeneratingUnitID
	left join dbo.Delineation d
		on lgu.DelineationID = d.DelineationID
	left join dbo.TreatmentBMP t 
		on t.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.HRUCharacteristicLandUseCode luc
		on hru.HRUCharacteristicLandUseCodeID = luc.HRUCharacteristicLandUseCodeID
	left join dbo.RegionalSubbasin rsb
		on rsb.RegionalSubbasinID = lgu.RegionalSubbasinID
	left join dbo.ModelBasin Model
		on Model.ModelBasinID = lgu.ModelBasinID
	where t.ProjectID is null