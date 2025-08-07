CREATE TABLE [dbo].[LoadGeneratingUnit](
	[LoadGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LoadGeneratingUnit_LoadGeneratingUnitID] PRIMARY KEY,
	[LoadGeneratingUnitGeometry] [geometry] NOT NULL,
	[ModelBasinID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit_ModelBasin_ModelBasinID] FOREIGN KEY REFERENCES [dbo].[ModelBasin] ([ModelBasinID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[IsEmptyResponseFromHRUService] [bit] NULL,
	[DateHRURequested] datetime null,
	[HRULogID] int null CONSTRAINT [FK_LoadGeneratingUnit_HRULog_HRULogID] FOREIGN KEY REFERENCES [dbo].[HRULog] ([HRULogID]),
    [LoadGeneratingUnitGeometry4326] [geometry] NULL,
)
GO

create spatial index [SPATIAL_LoadGeneratingUnit_LoadGeneratingUnitGeometry] on [dbo].[LoadGeneratingUnit]
(
	[LoadGeneratingUnitGeometry]
)
with (BOUNDING_BOX=(1.82653e+006, 636162, 1.89215e+006, 699352))
GO

create spatial index [SPATIAL_LoadGeneratingUnit_LoadGeneratingUnitGeometry4326] on [dbo].[LoadGeneratingUnit]
(
	[LoadGeneratingUnitGeometry4326]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)