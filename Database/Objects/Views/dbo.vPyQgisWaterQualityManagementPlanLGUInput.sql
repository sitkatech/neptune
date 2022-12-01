Drop View If Exists dbo.vPyQgisWaterQualityManagementPlanLGUInput
GO

Create view dbo.vPyQgisWaterQualityManagementPlanLGUInput
as 
Select 
	WaterQualityManagementPlanID as WQMPID,
	WaterQualityManagementPlanBoundary
from dbo.WaterQualityManagementPlan
Where WaterQualityManagementPlanBoundary is not null
	and WaterQualityManagementPlanModelingApproachID <> 1 -- exclude detailed WQMPs
GO