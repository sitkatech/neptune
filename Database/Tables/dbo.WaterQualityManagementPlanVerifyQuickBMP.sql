SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP](
	[WaterQualityManagementPlanVerifyQuickBMPID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL,
	[QuickBMPID] [int] NOT NULL,
	[IsAdequate] [bit] NULL,
	[WaterQualityManagementPlanVerifyQuickBMPNote] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerifyQuickBMPID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifyQuickBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMPID_WaterQualityManagementPlanVerifyQuickBMPID] UNIQUE NONCLUSTERED 
(
	[QuickBMPID] ASC,
	[WaterQualityManagementPlanVerifyQuickBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMP_QuickBMPID] FOREIGN KEY([QuickBMPID])
REFERENCES [dbo].[QuickBMP] ([QuickBMPID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMP_QuickBMPID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_Tenant_TenantID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY([WaterQualityManagementPlanVerifyID])
REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyQuickBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID]