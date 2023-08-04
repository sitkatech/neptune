CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP](
	[WaterQualityManagementPlanVerifyQuickBMPID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerifyQuickBMPID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID]),
	[QuickBMPID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMP_QuickBMPID] FOREIGN KEY REFERENCES [dbo].[QuickBMP] ([QuickBMPID]),
	[IsAdequate] [bit] NULL,
	[WaterQualityManagementPlanVerifyQuickBMPNote] [varchar](500) NULL,
	CONSTRAINT [AK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMPID_WaterQualityManagementPlanVerifyQuickBMPID] UNIQUE([QuickBMPID], [WaterQualityManagementPlanVerifyQuickBMPID])
)