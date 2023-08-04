CREATE TABLE [dbo].[TreatmentBMP](
	[TreatmentBMPID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMP_TreatmentBMPID] PRIMARY KEY,
	[TreatmentBMPName] [varchar](200),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMP_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[LocationPoint] [geometry] NULL,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[Notes] [varchar](1000) NULL,
	[SystemOfRecordID] [varchar](100) NULL,
	[YearBuilt] [int] NULL,
	[OwnerOrganizationID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID] FOREIGN KEY REFERENCES [dbo].[Organization] ([OrganizationID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_TreatmentBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[TreatmentBMPLifespanTypeID] [int] NULL CONSTRAINT [FK_TreatmentBMP_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPLifespanType] ([TreatmentBMPLifespanTypeID]),
	[TreatmentBMPLifespanEndDate] [datetime] NULL,
	[RequiredFieldVisitsPerYear] [int] NULL,
	[RequiredPostStormFieldVisitsPerYear] [int] NULL,
	[InventoryIsVerified] [bit] NOT NULL,
	[DateOfLastInventoryVerification] [datetime] NULL,
	[InventoryVerifiedByPersonID] [int] NULL CONSTRAINT [FK_TreatmentBMP_Person_InventoryVerifiedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[InventoryLastChangedDate] [datetime] NULL,
	[TrashCaptureStatusTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMP_TrashCaptureStatusType_TrashCaptureStatusTypeID] FOREIGN KEY REFERENCES [dbo].[TrashCaptureStatusType] ([TrashCaptureStatusTypeID]),
	[SizingBasisTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMP_SizingBasisType_SizingBasisTypeID] FOREIGN KEY REFERENCES [dbo].[SizingBasisType] ([SizingBasisTypeID]),
	[TrashCaptureEffectiveness] [int] NULL,
	[LocationPoint4326] [geometry] NULL,
	[WatershedID] [int] NULL CONSTRAINT [FK_TreatmentBMP_Watershed_WatershedID] FOREIGN KEY REFERENCES [dbo].[Watershed] ([WatershedID]),
	[ModelBasinID] [int] NULL CONSTRAINT [FK_TreatmentBMP_ModelBasin_ModelBasinID] FOREIGN KEY REFERENCES [dbo].[ModelBasin] ([ModelBasinID]),
	[PrecipitationZoneID] [int] NULL CONSTRAINT [FK_TreatmentBMP_PrecipitationZone_PrecipitationZoneID] FOREIGN KEY REFERENCES [dbo].[PrecipitationZone] ([PrecipitationZoneID]),
	[UpstreamBMPID] [int] NULL CONSTRAINT [FK_TreatmentBMP_TreatmentBMP_UpstreamBMPID_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[RegionalSubbasinID] [int] NULL,
	[ProjectID] [int] NULL CONSTRAINT [FK_TreatmentBMP_Project_ProjectID] FOREIGN KEY REFERENCES [dbo].[Project] ([ProjectID]),
	CONSTRAINT [AK_TreatmentBMP_StormwaterJurisdictionID_TreatmentBMPName] UNIQUE([StormwaterJurisdictionID], [TreatmentBMPName]),
	CONSTRAINT [AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] UNIQUE([TreatmentBMPID], [TreatmentBMPTypeID]),
	CONSTRAINT [CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate] CHECK  (([TreatmentBMPLifespanTypeID]=(3) AND [TreatmentBMPLifespanEndDate] IS NOT NULL OR [TreatmentBMPLifespanTypeID]<>(3) AND [TreatmentBMPLifespanEndDate] IS NULL)),
	CONSTRAINT [CK_TreatmentBMP_TrashCaptureEffectivenessMustBeBetween1And99] CHECK  (([TrashCaptureEffectiveness] IS NULL OR [TrashCaptureEffectiveness]>(0) AND [TrashCaptureEffectiveness]<(100)))
)
GO

CREATE SPATIAL INDEX [SPATIAL_TreatmentBMP_LocationPoint] ON [dbo].[TreatmentBMP]
(
	[LocationPoint]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-123, 33, -117, 46), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)