CREATE TABLE [dbo].[StormwaterJurisdictionGeometry](
	[StormwaterJurisdictionGeometryID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_StormwaterJurisdictionGeometry_StormwaterJurisdictionGeometryID] PRIMARY KEY,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [AK_StormwaterJurisdictionGeometry_StormwaterJurisdictionID] UNIQUE CONSTRAINT [FK_StormwaterJurisdictionGeometry_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[GeometryNative] [geometry] NOT NULL,
	[Geometry4326] [geometry] NOT NULL
)
GO

CREATE SPATIAL INDEX [SPATIAL_StormwaterJurisdictionGeometry_GeometryNative] ON [dbo].[StormwaterJurisdictionGeometry]
(
	[GeometryNative]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.82653e+006, 636160, 1.89215e+006, 698756), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE SPATIAL INDEX [SPATIAL_StormwaterJurisdictionGeometry_Geometry4326] ON [dbo].[StormwaterJurisdictionGeometry]
(
	[Geometry4326]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)