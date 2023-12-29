CREATE TABLE [dbo].[WaterQualityManagementPlanPriority](
	[WaterQualityManagementPlanPriorityID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID] PRIMARY KEY,
	[WaterQualityManagementPlanPriorityName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityName] UNIQUE,
	[WaterQualityManagementPlanPriorityDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityDisplayName] UNIQUE
)
