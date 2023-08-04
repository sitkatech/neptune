CREATE TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP](
	[WaterQualityManagementPlanVerifySourceControlBMPID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerifySourceControlBMPID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID]),
	[SourceControlBMPID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_SourceControlBMP_SourceControlBMPID] FOREIGN KEY REFERENCES [dbo].[SourceControlBMP] ([SourceControlBMPID]),
	[WaterQualityManagementPlanSourceControlCondition] [varchar](1000) NULL,
)