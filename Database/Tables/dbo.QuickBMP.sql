SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuickBMP](
	[QuickBMPID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[QuickBMPName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[QuickBMPNote] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_QuickBMP_QuickBMPID] PRIMARY KEY CLUSTERED 
(
	[QuickBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanID] ASC,
	[QuickBMPName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_Tenant_TenantID]
GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID]