drop view if exists dbo.vNereidLoadingInput
GO

create view dbo.vNereidLoadingInput
as
select
	hru.HRUCharacteristicID as PrimaryKey,
	DelineationID,
	WaterQualityManagementPlanID,
	rsb.RegionalSubbasinID, -- don't actually need this but it felt silly to leave it off
	OCSurveyCatchmentID,
	LSPCBasinKey,
	HRUCharacteristicLandUseCodeName,
	HydrologicSoilGroup,
	SlopePercentage,
	Area,
	ImperviousAcres
from
	dbo.HRUCharacteristic hru join dbo.LoadGeneratingUnit lgu
		on hru.LoadGeneratingUnitID = lgu.LoadGeneratingUnitID
	join dbo.RegionalSubbasin rsb
		on lgu.RegionalSubbasinID = rsb.RegionalSubbasinID
	join dbo.LSPCBasin lspc
		on lgu.LSPCBasinID = lspc.LSPCBasinID
	join HRUCharacteristicLandUseCode hrucode
		on hru.HRUCharacteristicLandUseCodeID = hrucode.HRUCharacteristicLandUseCodeID
GO