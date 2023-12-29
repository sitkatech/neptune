CREATE TABLE [dbo].[RegionalSubbasinStaging](
	[RegionalSubbasinStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_RegionalSubbasinStaging_RegionalSubbasinStagingID] PRIMARY KEY,
	[DrainID] [varchar](10) NULL,
	[Watershed] [varchar](100) NULL,
	[CatchmentGeometry] [geometry] NULL,
	[OCSurveyCatchmentID] [int] NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL
)
GO

CREATE SPATIAL INDEX [SPATIAL_RegionalSubbasinStaging_CatchmentGeometry] ON [dbo].[RegionalSubbasinStaging]
(
	[CatchmentGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.82653e+006, 636158, 1.89215e+006, 699352), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
