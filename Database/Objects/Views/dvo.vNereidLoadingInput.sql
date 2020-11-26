drop view if exists dbo.vNereidLoadingInput
GO

create view dbo.vNereidLoadingInput
as
select
	hru.HRUCharacteristicID as PrimaryKey,
	d.DelineationID,
	lgu.WaterQualityManagementPlanID,
	rsb.RegionalSubbasinID, -- don't actually need this but it felt silly to leave it off
	OCSurveyCatchmentID,
	LSPCBasinKey,
	hrucode.HRUCharacteristicLandUseCodeName as LandUseCode,
	basehrucode.HRUCharacteristicLandUseCodeDisplayName as BaselineLandUseCode,
	HydrologicSoilGroup,
	SlopePercentage,
	Area,
	ImperviousAcres,
	BaselineImperviousAcres,
	d.IsVerified as DelineationIsVerified,
	wqmp.WaterQualityManagementPlanModelingApproachID
from
	dbo.HRUCharacteristic hru join dbo.LoadGeneratingUnit lgu
		on hru.LoadGeneratingUnitID = lgu.LoadGeneratingUnitID
	join dbo.RegionalSubbasin rsb
		on lgu.RegionalSubbasinID = rsb.RegionalSubbasinID
	join dbo.LSPCBasin lspc
		on lgu.LSPCBasinID = lspc.LSPCBasinID
	join dbo.HRUCharacteristicLandUseCode hrucode
		on hru.HRUCharacteristicLandUseCodeID = hrucode.HRUCharacteristicLandUseCodeID
	join dbo.HRUCharacteristicLandUseCode basehrucode
		on hru.BaselineHRUCharacteristicLandUseCodeID = basehrucode.HRUCharacteristicLandUseCodeID
	left join dbo.Delineation d
		on d.DelineationID = lgu.DelineationID
	left join dbo.WaterQualityManagementPlan wqmp
		on lgu.WaterQualityManagementPlanID = lgu.WaterQualityManagementPlanID
GO

