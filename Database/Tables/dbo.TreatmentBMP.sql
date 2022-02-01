SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMP](
	[TreatmentBMPID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[LocationPoint] [geometry] NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[Notes] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SystemOfRecordID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YearBuilt] [int] NULL,
	[OwnerOrganizationID] [int] NOT NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[TreatmentBMPLifespanTypeID] [int] NULL,
	[TreatmentBMPLifespanEndDate] [datetime] NULL,
	[RequiredFieldVisitsPerYear] [int] NULL,
	[RequiredPostStormFieldVisitsPerYear] [int] NULL,
	[InventoryIsVerified] [bit] NOT NULL,
	[DateOfLastInventoryVerification] [datetime] NULL,
	[InventoryVerifiedByPersonID] [int] NULL,
	[InventoryLastChangedDate] [datetime] NULL,
	[TrashCaptureStatusTypeID] [int] NOT NULL,
	[SizingBasisTypeID] [int] NOT NULL,
	[TrashCaptureEffectiveness] [int] NULL,
	[LocationPoint4326] [geometry] NULL,
	[WatershedID] [int] NULL,
	[LSPCBasinID] [int] NULL,
	[PrecipitationZoneID] [int] NULL,
	[UpstreamBMPID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[ProjectID] [int] NULL,
 CONSTRAINT [PK_TreatmentBMP_TreatmentBMPID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMP_StormwaterJurisdictionID_TreatmentBMPName] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionID] ASC,
	[TreatmentBMPName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[TreatmentBMPTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_LSPCBasin_LSPCBasinID] FOREIGN KEY([LSPCBasinID])
REFERENCES [dbo].[LSPCBasin] ([LSPCBasinID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_LSPCBasin_LSPCBasinID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID] FOREIGN KEY([OwnerOrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Person_InventoryVerifiedByPersonID_PersonID] FOREIGN KEY([InventoryVerifiedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Person_InventoryVerifiedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_PrecipitationZone_PrecipitationZoneID] FOREIGN KEY([PrecipitationZoneID])
REFERENCES [dbo].[PrecipitationZone] ([PrecipitationZoneID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_PrecipitationZone_PrecipitationZoneID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Project_ProjectID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_SizingBasisType_SizingBasisTypeID] FOREIGN KEY([SizingBasisTypeID])
REFERENCES [dbo].[SizingBasisType] ([SizingBasisTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_SizingBasisType_SizingBasisTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_TrashCaptureStatusType_TrashCaptureStatusTypeID] FOREIGN KEY([TrashCaptureStatusTypeID])
REFERENCES [dbo].[TrashCaptureStatusType] ([TrashCaptureStatusTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_TrashCaptureStatusType_TrashCaptureStatusTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_TreatmentBMP_UpstreamBMPID_TreatmentBMPID] FOREIGN KEY([UpstreamBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_TreatmentBMP_UpstreamBMPID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeID] FOREIGN KEY([TreatmentBMPLifespanTypeID])
REFERENCES [dbo].[TreatmentBMPLifespanType] ([TreatmentBMPLifespanTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeID]
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
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMP_Watershed_WatershedID] FOREIGN KEY([WatershedID])
REFERENCES [dbo].[Watershed] ([WatershedID])
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [FK_TreatmentBMP_Watershed_WatershedID]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate] CHECK  (([TreatmentBMPLifespanTypeID]=(3) AND [TreatmentBMPLifespanEndDate] IS NOT NULL OR [TreatmentBMPLifespanTypeID]<>(3) AND [TreatmentBMPLifespanEndDate] IS NULL))
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate]
GO
ALTER TABLE [dbo].[TreatmentBMP]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMP_TrashCaptureEffectivenessMustBeBetween1And99] CHECK  (([TrashCaptureEffectiveness] IS NULL OR [TrashCaptureEffectiveness]>(0) AND [TrashCaptureEffectiveness]<(100)))
GO
ALTER TABLE [dbo].[TreatmentBMP] CHECK CONSTRAINT [CK_TreatmentBMP_TrashCaptureEffectivenessMustBeBetween1And99]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_TreatmentBMP_LocationPoint] ON [dbo].[TreatmentBMP]
(
	[LocationPoint]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-123, 33, -117, 46), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]