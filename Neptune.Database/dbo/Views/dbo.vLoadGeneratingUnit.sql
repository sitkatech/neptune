Create view dbo.vLoadGeneratingUnit
as

select  lgu.LoadGeneratingUnitID, lgu.DateHRURequested, lgu.IsEmptyResponseFromHRUService, lgu.DelineationID,
        tb.TreatmentBMPID, tb.TreatmentBMPName, wqmp.WaterQualityManagementPlanID, wqmp.WaterQualityManagementPlanName,
        rsb.RegionalSubbasinID, concat(rsb.Watershed, ' - ', rsb.DrainID, ': ', rsb.RegionalSubbasinID) as RegionalSubbasinName,
        mb.ModelBasinID, mb.ModelBasinKey
from dbo.LoadGeneratingUnit lgu
left join dbo.Delineation d on lgu.DelineationID = d.DelineationID
left join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
left join dbo.WaterQualityManagementPlan wqmp on lgu.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
left join dbo.RegionalSubbasin rsb on lgu.RegionalSubbasinID = rsb.RegionalSubbasinID
left join dbo.ModelBasin mb on lgu.ModelBasinID = mb.ModelBasinID
GO