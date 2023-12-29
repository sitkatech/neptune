CREATE TABLE [dbo].[WaterQualityManagementPlanStatus](
	[WaterQualityManagementPlanStatusID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusID] PRIMARY KEY,
	[WaterQualityManagementPlanStatusName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusName] UNIQUE,
	[WaterQualityManagementPlanStatusDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusDisplayName] UNIQUE
)
