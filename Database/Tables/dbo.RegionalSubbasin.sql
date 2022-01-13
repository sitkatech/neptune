SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegionalSubbasin](
	[RegionalSubbasinID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CatchmentGeometry] [geometry] NOT NULL,
	[OCSurveyCatchmentID] [int] NOT NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL,
	[CatchmentGeometry4326] [geometry] NULL,
	[LastUpdate] [datetime] NULL,
	[IsWaitingForLGURefresh] [bit] NULL,
	[IsInLSPCBasin] [bit] NULL,
	[LSPCBasinID] [int] NULL,
 CONSTRAINT [PK_RegionalSubbasin_RegionalSubbasinID] PRIMARY KEY CLUSTERED 
(
	[RegionalSubbasinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_RegionalSubbasin_OCSurveyCatchmentID] UNIQUE NONCLUSTERED 
(
	[OCSurveyCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX [IX_RegionalSubbasin_OCSurveyDownstreamCatchmentID] ON [dbo].[RegionalSubbasin]
(
	[OCSurveyDownstreamCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RegionalSubbasin]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasin_LSPCBasin_LSPCBasinID] FOREIGN KEY([LSPCBasinID])
REFERENCES [dbo].[LSPCBasin] ([LSPCBasinID])
GO
ALTER TABLE [dbo].[RegionalSubbasin] CHECK CONSTRAINT [FK_RegionalSubbasin_LSPCBasin_LSPCBasinID]
GO
ALTER TABLE [dbo].[RegionalSubbasin]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID] FOREIGN KEY([OCSurveyDownstreamCatchmentID])
REFERENCES [dbo].[RegionalSubbasin] ([OCSurveyCatchmentID])
GO
ALTER TABLE [dbo].[RegionalSubbasin] CHECK CONSTRAINT [FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_RegionalSubbasin_CatchmentGeometry] ON [dbo].[RegionalSubbasin]
(
	[CatchmentGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]