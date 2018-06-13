SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlan](
	[WaterQualityManagementPlanID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[WaterQualityManagementPlanLandUseID] [int] NOT NULL,
	[WaterQualityManagementPlanPriorityID] [int] NOT NULL,
	[WaterQualityManagementPlanStatusID] [int] NOT NULL,
	[WaterQualityManagementPlanDevelopmentTypeID] [int] NOT NULL,
	[WaterQualityManagementPlanName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ApprovalDate] [datetime] NULL,
	[MaintenanceContactName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactOrganization] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactPhone] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactAddress1] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactAddress2] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactCity] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactState] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceContactZip] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WaterQualityManagementPlan_WaterQualityManagementPlanID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlan_WaterQualityManagementPlanID_TenantID] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlan_WaterQualityManagementPlanName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID] FOREIGN KEY([StormwaterJurisdictionID], [TenantID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID], [TenantID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_Tenant_TenantID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanDevelopmentTypeID] FOREIGN KEY([WaterQualityManagementPlanDevelopmentTypeID])
REFERENCES [dbo].[WaterQualityManagementPlanDevelopmentType] ([WaterQualityManagementPlanDevelopmentTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanDevelopmentTypeID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID] FOREIGN KEY([WaterQualityManagementPlanLandUseID])
REFERENCES [dbo].[WaterQualityManagementPlanLandUse] ([WaterQualityManagementPlanLandUseID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID] FOREIGN KEY([WaterQualityManagementPlanPriorityID])
REFERENCES [dbo].[WaterQualityManagementPlanPriority] ([WaterQualityManagementPlanPriorityID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanStatusID] FOREIGN KEY([WaterQualityManagementPlanStatusID])
REFERENCES [dbo].[WaterQualityManagementPlanStatus] ([WaterQualityManagementPlanStatusID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanStatusID]