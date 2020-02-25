SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdictionGeometry](
	[StormwaterJurisdictionGeometryID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[GeometryNative] [geometry] NOT NULL,
	[Geometry4326] [geometry] NOT NULL,
 CONSTRAINT [PK_StormwaterJurisdictionGeometry_StormwaterJurisdictionGeometryID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionGeometryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdictionGeometry_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterJurisdictionGeometry]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionGeometry_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionGeometry] CHECK CONSTRAINT [FK_StormwaterJurisdictionGeometry_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_StormwaterJurisdictionGeometry_GeometryNative] ON [dbo].[StormwaterJurisdictionGeometry]
(
	[GeometryNative]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]