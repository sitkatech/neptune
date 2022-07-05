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
	[HydromodificationAppliesTypeID] [int] NULL,
	[DateOfContruction] [datetime] NULL,
	[HydrologicSubareaID] [int] NULL,
	[RecordNumber] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RecordedWQMPAreaInAcres] [decimal](5, 1) NULL,
	[TrashCaptureStatusTypeID] [int] NOT NULL,
	[TrashCaptureEffectiveness] [int] NULL,
	[WaterQualityManagementPlanBoundary] [geometry] NULL,
	[WaterQualityManagementPlanModelingApproachID] [int] NOT NULL,
	[WaterQualityManagementPlanBoundary4326] [geometry] NULL,
	[WaterQualityManagementPlanAreaInAcres] [float] NULL,
 CONSTRAINT [PK_WaterQualityManagementPlan_WaterQualityManagementPlanID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlan_WaterQualityManagementPlanName_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanName] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID] FOREIGN KEY([HydrologicSubareaID])
REFERENCES [dbo].[HydrologicSubarea] ([HydrologicSubareaID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_HydromodificationAppliesType_HydromodificationAppliesTypeID] FOREIGN KEY([HydromodificationAppliesTypeID])
REFERENCES [dbo].[HydromodificationAppliesType] ([HydromodificationAppliesTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_HydromodificationAppliesType_HydromodificationAppliesTypeID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_TrashCaptureStatusType_TrashCaptureStatusTypeID] FOREIGN KEY([TrashCaptureStatusTypeID])
REFERENCES [dbo].[TrashCaptureStatusType] ([TrashCaptureStatusTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_TrashCaptureStatusType_TrashCaptureStatusTypeID]
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
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID] FOREIGN KEY([WaterQualityManagementPlanModelingApproachID])
REFERENCES [dbo].[WaterQualityManagementPlanModelingApproach] ([WaterQualityManagementPlanModelingApproachID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID]
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
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan]  WITH CHECK ADD  CONSTRAINT [CK_WaterQualityManagementPlan_TrashCaptureEffectivenessMustBeBetween1And99] CHECK  (([TrashCaptureEffectiveness] IS NULL OR [TrashCaptureEffectiveness]>(0) AND [TrashCaptureEffectiveness]<(100)))
GO
ALTER TABLE [dbo].[WaterQualityManagementPlan] CHECK CONSTRAINT [CK_WaterQualityManagementPlan_TrashCaptureEffectivenessMustBeBetween1And99]