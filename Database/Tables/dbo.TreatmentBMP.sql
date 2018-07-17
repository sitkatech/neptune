SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMP](
	[TreatmentBMPID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[LocationPoint] [geometry] NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[ModeledCatchmentID] [int] NULL,
	[Notes] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SystemOfRecordID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YearBuilt] [int] NULL,
	[OwnerOrganizationID] [int] NOT NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[TreatmentBMPLifespanTypeID] [int] NULL,
	[TreatmentBMPLifespanEndDate] [datetime] NULL,
 CONSTRAINT [PK_TreatmentBMP_TreatmentBMPID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMP_TreatmentBMPID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[TreatmentBMPTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMP_TreatmentBMPName_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPName] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID] FOREIGN KEY([ModeledCatchmentID])
REFERENCES [dbo].[ModeledCatchment] ([ModeledCatchmentID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID_TenantID] FOREIGN KEY([ModeledCatchmentID], [TenantID])
REFERENCES [dbo].[ModeledCatchment] ([ModeledCatchmentID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID] FOREIGN KEY([OwnerOrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID] FOREIGN KEY([StormwaterJurisdictionID], [TenantID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_TreatmentBMPLifespanType] FOREIGN KEY([TreatmentBMPLifespanTypeID])
REFERENCES [dbo].[TreatmentBMPLifespanType] ([TreatmentBMPLifespanTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_TreatmentBMPLifespanType]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID_TenantID] FOREIGN KEY([WaterQualityManagementPlanID], [TenantID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate] CHECK  (([TreatmentBMPLifespanTypeID]=(3) AND [TreatmentBMPLifespanEndDate] IS NOT NULL OR [TreatmentBMPLifespanTypeID]<>(3) AND [TreatmentBMPLifespanEndDate] IS NULL))
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate]