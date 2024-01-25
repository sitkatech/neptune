CREATE TABLE [dbo].[SpatialGridUnit](
	[SpatialGridUnitID] [int] NOT NULL CONSTRAINT [PK_SpatialGridUnit_SpatialGridUnitID] PRIMARY KEY,
	[SpatialGridUnitGeometry] [geometry] NOT NULL
)
GO

create spatial index [SPATIAL_SpatialGridUnit_SpatialGridUnitGeometry] on [dbo].[SpatialGridUnit]
(
	[SpatialGridUnitGeometry]
)
with (BOUNDING_BOX=(1.82653e+006, 636160, 1.89215e+006, 698756))
