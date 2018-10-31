SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP](
	[WaterQualityManagementPlanVerifyTreatmentBMPID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[IsAdequate] [bit] NULL,
	[WaterQualityManagementPlanVerifyTreatmentBMPNote] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerifyTreatmentBMPID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifyTreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMPID_WaterQualityManagementPlanVerifyTreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[WaterQualityManagementPlanVerifyTreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_Tenant_TenantID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY([WaterQualityManagementPlanVerifyID])
REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID]