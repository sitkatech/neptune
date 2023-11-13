CREATE TABLE [dbo].[Watershed](
	[WatershedID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Watershed_WatershedID] PRIMARY KEY,
	[WatershedGeometry] [geometry] NULL,
	[WatershedName] [varchar](50) NULL,
	[WatershedGeometry4326] [geometry] NULL
)
GO

create spatial index [SPATIAL_Watershed_WatershedGeometry] on [dbo].[Watershed]
(
	[WatershedGeometry]
)
with (BOUNDING_BOX=(1.82653e+006, 636158, 1.89215e+006, 699352))
GO

create spatial index [SPATIAL_Watershed_WatershedGeometry4326] on [dbo].[Watershed]
(
	[WatershedGeometry4326]
)
with (BOUNDING_BOX=(-119, 33, -117, 34))
