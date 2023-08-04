CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyType](
	[WaterQualityManagementPlanVerifyTypeID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyTypeName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeName] UNIQUE,
	[WaterQualityManagementPlanVerifyTypeDisplayName] [varchar](100)
)
