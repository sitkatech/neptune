CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP](
	[WaterQualityManagementPlanVerifyTreatmentBMPID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerifyTreatmentBMPID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID]),
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[IsAdequate] [bit] NULL,
	[WaterQualityManagementPlanVerifyTreatmentBMPNote] [varchar](500) NULL,
	CONSTRAINT [AK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMPID_WaterQualityManagementPlanVerifyTreatmentBMPID] UNIQUE ([TreatmentBMPID], [WaterQualityManagementPlanVerifyTreatmentBMPID])
)