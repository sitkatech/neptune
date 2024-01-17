Create view dbo.vHRUCharacteristic
as

select  hru.HRUCharacteristicID, hru.LastUpdated, hru.LoadGeneratingUnitID, 
        case
            when tb.TreatmentBMPID is not null then 'Treatment BMP'
            when wqmp.WaterQualityManagementPlanID is not null then 'Water Quality Management Plan'
            else 'Regional Subbasin'
        end as HRUEntity,
        tb.TreatmentBMPID, tb.TreatmentBMPName, wqmp.WaterQualityManagementPlanID, wqmp.WaterQualityManagementPlanName,
        rsb.RegionalSubbasinID, concat(rsb.Watershed, ' - ', rsb.DrainID, ': ', rsb.RegionalSubbasinID) as RegionalSubbasinName,
        hru.HydrologicSoilGroup, hru.SlopePercentage, hru.Area, 
        hru.ImperviousAcres, hru.HRUCharacteristicLandUseCodeID, hruc.HRUCharacteristicLandUseCodeDisplayName, 
        hru.BaselineImperviousAcres, hru.BaselineHRUCharacteristicLandUseCodeID, bhruc.HRUCharacteristicLandUseCodeDisplayName as BaslineHRUCharacteristicLandUseCodeDisplayName
from dbo.HRUCharacteristic hru
join dbo.HRUCharacteristicLandUseCode hruc on hru.HRUCharacteristicLandUseCodeID = hruc.HRUCharacteristicLandUseCodeID
join dbo.HRUCharacteristicLandUseCode bhruc on hru.BaselineHRUCharacteristicLandUseCodeID = bhruc.HRUCharacteristicLandUseCodeID
join dbo.LoadGeneratingUnit lgu on hru.LoadGeneratingUnitID = lgu.LoadGeneratingUnitID
left join dbo.Delineation d on lgu.DelineationID = d.DelineationID
left join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
left join dbo.WaterQualityManagementPlan wqmp on lgu.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
left join dbo.RegionalSubbasin rsb on lgu.RegionalSubbasinID = rsb.RegionalSubbasinID
GO