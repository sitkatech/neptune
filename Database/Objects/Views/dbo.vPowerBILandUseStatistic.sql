drop view if exists dbo.vPowerBILandUseStatistic
GO

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
	lgu.LSPCBasinID,
	rsb.Watershed as WatershedName,
	rsb.OCSurveyCatchmentID as CatchIDN,
	rsb.OCSurveyDownstreamCatchmentID as DownCatchIDN,
	d.TreatmentBMPID,
	d.DelineationID,
	lgu.WaterQualityManagementPlanID,
	lgu.RegionalSubbasinID,
	lgu.LoadGeneratingUnitID,
	lspc.LSPCBasinName,
	luc.HRUCharacteristicLandUseCodeName as LandUse,
	concat(lspc.LSPCBasinKey, '-', luc.HRUCharacteristicLandUseCodeName, '-', hru.HydrologicSoilGroup, '-', hru.SlopePercentage) as SurfaceKey
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
	left join dbo.LSPCBasin lspc
		on lspc.LSPCBasinID = lgu.LSPCBasinID
	where t.ProjectID is null