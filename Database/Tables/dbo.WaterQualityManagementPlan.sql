SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlan](
	[WaterQualityManagementPlanID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[WaterQualityManagementPlanLandUseID] [int] NULL,
	[WaterQualityManagementPlanPriorityID] [int] NULL,
	[WaterQualityManagementPlanStatusID] [int] NULL,
	[WaterQualityManagementPlanDevelopmentTypeID] [int] NULL,
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
	[WaterQualityManagementPlanPermitTermID] [int] NULL,
	[HydromodificationAppliesID] [int] NULL,
	[DateOfContruction] [datetime] NULL,
	[HydrologicSubareaID] [int] NULL,
	[RecordNumber] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RecordedWQMPAreaInAcres] [decimal](5, 1) NULL,
 CONSTRAINT [PK_WaterQualityManagementPlan_WaterQualityManagementPlanID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlan_WaterQualityManagementPlanName_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanName] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID] FOREIGN KEY([HydrologicSubareaID])
REFERENCES [dbo].[HydrologicSubarea] ([HydrologicSubareaID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_HydromodificationApplies_HydromodificationAppliesID] FOREIGN KEY([HydromodificationAppliesID])
REFERENCES [dbo].[HydromodificationApplies] ([HydromodificationAppliesID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_HydromodificationApplies_HydromodificationAppliesID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeID] FOREIGN KEY([WaterQualityManagementPlanDevelopmentTypeID])
REFERENCES [dbo].[WaterQualityManagementPlanDevelopmentType] ([WaterQualityManagementPlanDevelopmentTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID] FOREIGN KEY([WaterQualityManagementPlanLandUseID])
REFERENCES [dbo].[WaterQualityManagementPlanLandUse] ([WaterQualityManagementPlanLandUseID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID] FOREIGN KEY([WaterQualityManagementPlanPermitTermID])
REFERENCES [dbo].[WaterQualityManagementPlanPermitTerm] ([WaterQualityManagementPlanPermitTermID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID] FOREIGN KEY([WaterQualityManagementPlanPriorityID])
REFERENCES [dbo].[WaterQualityManagementPlanPriority] ([WaterQualityManagementPlanPriorityID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusID] FOREIGN KEY([WaterQualityManagementPlanStatusID])
REFERENCES [dbo].[WaterQualityManagementPlanStatus] ([WaterQualityManagementPlanStatusID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusID]