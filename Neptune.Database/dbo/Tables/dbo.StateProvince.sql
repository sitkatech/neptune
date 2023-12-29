CREATE TABLE [dbo].[StateProvince](
	[StateProvinceID] [int] NOT NULL CONSTRAINT [PK_StateProvince_StateProvinceID] PRIMARY KEY,
	[StateProvinceName] [varchar](100),
	[StateProvinceAbbreviation] [char](2),
	[StateProvinceFeature] [geometry] NULL,
	[StateProvinceFeatureForAnalysis] [geometry] NOT NULL
)
GO

CREATE SPATIAL INDEX [SPATIAL_StateProvince_StateProvinceFeature] ON [dbo].[StateProvince]
(
	[StateProvinceFeature]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-121, 38, -119, 40), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
CREATE SPATIAL INDEX [SPATIAL_StateProvince_StateProvinceFeatureForAnalysis] ON [dbo].[StateProvince]
(
	[StateProvinceFeatureForAnalysis]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-125, 35, -115, 43), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)