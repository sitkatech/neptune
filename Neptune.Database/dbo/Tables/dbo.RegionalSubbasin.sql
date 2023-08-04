CREATE TABLE [dbo].[RegionalSubbasin](
	[RegionalSubbasinID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_RegionalSubbasin_RegionalSubbasinID] PRIMARY KEY,
	[DrainID] [varchar](10) NULL,
	[Watershed] [varchar](100) NULL,
	[CatchmentGeometry] [geometry] NOT NULL,
	[OCSurveyCatchmentID] [int] NOT NULL CONSTRAINT [AK_RegionalSubbasin_OCSurveyCatchmentID] UNIQUE,
	[OCSurveyDownstreamCatchmentID] [int] NULL CONSTRAINT [FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([OCSurveyCatchmentID]),
	[CatchmentGeometry4326] [geometry] NULL,
	[LastUpdate] [datetime] NULL,
	[IsWaitingForLGURefresh] [bit] NULL,
	[IsInModelBasin] [bit] NULL,
	[ModelBasinID] [int] NULL CONSTRAINT [FK_RegionalSubbasin_ModelBasin_ModelBasinID] FOREIGN KEY REFERENCES [dbo].[ModelBasin] ([ModelBasinID])
)

GO
CREATE NONCLUSTERED INDEX [IX_RegionalSubbasin_OCSurveyDownstreamCatchmentID] ON [dbo].[RegionalSubbasin]
(
	[OCSurveyDownstreamCatchmentID] ASC
)
GO


CREATE SPATIAL INDEX [SPATIAL_RegionalSubbasin_CatchmentGeometry] ON [dbo].[RegionalSubbasin]
(
	[CatchmentGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)