CREATE TABLE [dbo].[WaterQualityManagementPlanVisitStatus](
	[WaterQualityManagementPlanVisitStatusID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID] PRIMARY KEY,
	[WaterQualityManagementPlanVisitStatusName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusName] UNIQUE,
	[WaterQualityManagementPlanVisitStatusDisplayName] [varchar](100)
)
