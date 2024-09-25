CREATE TABLE [dbo].[WaterQualityManagementPlanNereidLog](
	[WaterQualityManagementPlanNereidLogID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanNereidLog_WaterQualityManagementPlanNereidLogID] PRIMARY KEY,
	[WaterQualityManagementPlanID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanNereidLog_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[LastRequestDate] DATETIME NULL,
    [NereidRequest] VARCHAR(MAX) NULL,
    [NereidResponse] VARCHAR(MAX) NULL,
	CONSTRAINT [AK_WaterQualityManagementPlanNereidLog_WaterQualityManagementPlanID] UNIQUE([WaterQualityManagementPlanID])
)