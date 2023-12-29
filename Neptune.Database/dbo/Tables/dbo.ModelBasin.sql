CREATE TABLE [dbo].[ModelBasin](
	[ModelBasinID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ModelBasin_ModelBasinID] PRIMARY KEY,
	[ModelBasinKey] [int] NOT NULL CONSTRAINT [AK_ModelBasin_ModelBasinKey] UNIQUE,
	[ModelBasinGeometry] [geometry] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[ModelBasinState] [varchar](5),
	[ModelBasinRegion] [varchar](10)
)
GO

CREATE SPATIAL INDEX [SPATIAL_ModelBasin_ModelBasinGeometry] ON [dbo].[ModelBasin]
(
	[ModelBasinGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.82653e+006, 636160, 1.89619e+006, 699354), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
