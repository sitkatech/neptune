CREATE TABLE [dbo].[WaterQualityManagementPlan](
	[WaterQualityManagementPlanID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlan_WaterQualityManagementPlanID] PRIMARY KEY,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlan_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[WaterQualityManagementPlanLandUseID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanLandUse] ([WaterQualityManagementPlanLandUseID]),
	[WaterQualityManagementPlanPriorityID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanPriority] ([WaterQualityManagementPlanPriorityID]),
	[WaterQualityManagementPlanStatusID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanStatus] ([WaterQualityManagementPlanStatusID]),
	[WaterQualityManagementPlanDevelopmentTypeID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanDevelopmentType] ([WaterQualityManagementPlanDevelopmentTypeID]),
	[WaterQualityManagementPlanName] [varchar](100),
	[ApprovalDate] [datetime] NULL,
	[MaintenanceContactName] [varchar](100) NULL,
	[MaintenanceContactOrganization] [varchar](100) NULL,
	[MaintenanceContactPhone] [varchar](100) NULL,
	[MaintenanceContactAddress1] [varchar](100) NULL,
	[MaintenanceContactAddress2] [varchar](100) NULL,
	[MaintenanceContactCity] [varchar](100) NULL,
	[MaintenanceContactState] [varchar](100) NULL,
	[MaintenanceContactZip] [varchar](100) NULL,
	[WaterQualityManagementPlanPermitTermID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanPermitTerm] ([WaterQualityManagementPlanPermitTermID]),
	[HydromodificationAppliesTypeID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_HydromodificationAppliesType_HydromodificationAppliesTypeID] FOREIGN KEY REFERENCES [dbo].[HydromodificationAppliesType] ([HydromodificationAppliesTypeID]),
	[DateOfConstruction] [datetime] NULL,
	[HydrologicSubareaID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID] FOREIGN KEY REFERENCES [dbo].[HydrologicSubarea] ([HydrologicSubareaID]),
	[RecordNumber] [varchar](500) NULL,
	[RecordedWQMPAreaInAcres] [decimal](6, 2) NULL,
	[TrashCaptureStatusTypeID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlan_TrashCaptureStatusType_TrashCaptureStatusTypeID] FOREIGN KEY REFERENCES [dbo].[TrashCaptureStatusType] ([TrashCaptureStatusTypeID]),
	[TrashCaptureEffectiveness] [int] NULL CONSTRAINT [CK_WaterQualityManagementPlan_TrashCaptureEffectivenessMustBeBetween1And99] CHECK  (([TrashCaptureEffectiveness] IS NULL OR [TrashCaptureEffectiveness]>(0) AND [TrashCaptureEffectiveness]<(100))),
	[WaterQualityManagementPlanModelingApproachID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlan_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanModelingApproach] ([WaterQualityManagementPlanModelingApproachID]),
	[LastNereidLogID] [int] NULL CONSTRAINT [FK_WaterQualityManagementPlan_NereidLog_LastNereidLogID_NereidLogID] FOREIGN KEY REFERENCES [dbo].NereidLog ([NereidLogID]),
	[WaterQualityManagementPlanBoundaryNotes] varchar(500) NULL,
	CONSTRAINT [AK_WaterQualityManagementPlan_WaterQualityManagementPlanName_StormwaterJurisdictionID] UNIQUE ([WaterQualityManagementPlanName], [StormwaterJurisdictionID])
)