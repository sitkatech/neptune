CREATE TABLE [dbo].[LoadGeneratingUnit4326](
	[LoadGeneratingUnit4326ID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LoadGeneratingUnit4326_LoadGeneratingUnit4326ID] PRIMARY KEY,
	[LoadGeneratingUnit4326Geometry] [geometry] NOT NULL,
	[ModelBasinID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit4326_ModelBasin_ModelBasinID] FOREIGN KEY REFERENCES [dbo].[ModelBasin] ([ModelBasinID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit4326_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit4326_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_LoadGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[IsEmptyResponseFromHRUService] [bit] NULL,
	[DateHRURequested] datetime null
)
GO

create spatial index [SPATIAL_LoadGeneratingUnit4326_LoadGeneratingUnit4326Geometry] on [dbo].[LoadGeneratingUnit4326]
(
	[LoadGeneratingUnit4326Geometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)