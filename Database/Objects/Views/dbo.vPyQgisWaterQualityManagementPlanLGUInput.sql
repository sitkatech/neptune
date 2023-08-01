Drop View If Exists dbo.vPyQgisWaterQualityManagementPlanLGUInput
GO

Create view dbo.vPyQgisWaterQualityManagementPlanLGUInput
as 
Select 
	wqmp.WaterQualityManagementPlanID as WQMPID,
	wqmpb.GeometryNative as WaterQualityManagementPlanBoundary
from dbo.WaterQualityManagementPlanBoundary wqmpb
join dbo.WaterQualityManagementPlan wqmp on wqmpb.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
Where GeometryNative is not null
	and wqmp.WaterQualityManagementPlanModelingApproachID <> 1 -- exclude detailed WQMPs
GO