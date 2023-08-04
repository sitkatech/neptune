CREATE TABLE [dbo].[Watershed](
	[WatershedID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Watershed_WatershedID] PRIMARY KEY,
	[WatershedGeometry] [geometry] NULL,
	[WatershedName] [varchar](50) NULL,
	[WatershedGeometry4326] [geometry] NULL
)
