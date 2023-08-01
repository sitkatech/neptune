SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanBoundary](
	[WaterQualityManagementPlanGeometryID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[GeometryNative] [geometry] NULL,
	[Geometry4326] [geometry] NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanGeometryID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanGeometryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanID] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanBoundary]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanBoundary] CHECK CONSTRAINT [FK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_WaterQualityManagementPlanBoundary_Geometry4326] ON [dbo].[WaterQualityManagementPlanBoundary]
(
	[Geometry4326]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_WaterQualityManagementPlanBoundary_GeometryNative] ON [dbo].[WaterQualityManagementPlanBoundary]
(
	[GeometryNative]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1830180, 637916, 1880090, 690975), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]