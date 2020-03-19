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
	w.WatershedName,
	d.TreatmentBMPID,
	lgu.WaterQualityManagementPlanID,
	lgu.RegionalSubbasinID
from
	dbo.HRUCharacteristic hru left join dbo.LoadGeneratingUnit lgu
		on hru.LoadGeneratingUnitID = lgu.LoadGeneratingUnitID
	left join dbo.Delineation d
		on lgu.DelineationID = d.DelineationID
	left join dbo.TreatmentBMP t 
		on t.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.Watershed w
		on t.WatershedID = w.WatershedID
	left join dbo.HRUCharacteristicLandUseCode luc
		on hru.HRUCharacteristicLandUseCodeID = luc.HRUCharacteristicLandUseCodeID