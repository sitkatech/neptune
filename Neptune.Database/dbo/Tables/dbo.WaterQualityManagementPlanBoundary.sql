CREATE TABLE [dbo].[WaterQualityManagementPlanBoundary](
	[WaterQualityManagementPlanGeometryID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanGeometryID] PRIMARY KEY,
	[WaterQualityManagementPlanID] [int] NOT NULL CONSTRAINT [AK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanID] UNIQUE CONSTRAINT [FK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[GeometryNative] [geometry] NULL,
	[Geometry4326] [geometry] NULL
)
GO

CREATE SPATIAL INDEX [SPATIAL_WaterQualityManagementPlanBoundary_Geometry4326] ON [dbo].[WaterQualityManagementPlanBoundary]
(
	[Geometry4326]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE SPATIAL INDEX [SPATIAL_WaterQualityManagementPlanBoundary_GeometryNative] ON [dbo].[WaterQualityManagementPlanBoundary]
(
	[GeometryNative]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1830180, 637916, 1880090, 690975), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)