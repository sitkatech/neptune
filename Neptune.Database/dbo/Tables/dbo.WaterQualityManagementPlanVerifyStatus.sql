CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyStatus](
	[WaterQualityManagementPlanVerifyStatusID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyStatusName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusName] UNIQUE,
	[WaterQualityManagementPlanVerifyStatusDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusDisplayName] UNIQUE
)
