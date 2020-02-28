Drop View If Exists dbo.vWaterQualityManagementPlanLGUInput
GO

Create view dbo.vWaterQualityManagementPlanLGUInput
as 
Select 
	WaterQualityManagementPlanID as WQMPID,
	WaterQualityManagementPlanBoundary
from dbo.WaterQualityManagementPlan
Where WaterQualityManagementPlanBoundary is not null
GO
