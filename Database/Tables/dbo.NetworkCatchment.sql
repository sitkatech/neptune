SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkCatchment](
	[NetworkCatchmentID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CatchmentGeometry] [geometry] NOT NULL,
	[OCSurveyCatchmentID] [int] NOT NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL,
	[CatchmentGeometry4326] [geometry] NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_NetworkCatchment_NetworkCatchmentID] PRIMARY KEY CLUSTERED 
(
	[NetworkCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NetworkCatchment_OCSurveyCatchmentID] UNIQUE NONCLUSTERED 
(
	[OCSurveyCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[NetworkCatchment]  WITH CHECK ADD  CONSTRAINT [FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID] FOREIGN KEY([OCSurveyDownstreamCatchmentID])
REFERENCES [dbo].[NetworkCatchment] ([OCSurveyCatchmentID])
GO
ALTER TABLE [dbo].[NetworkCatchment] CHECK CONSTRAINT [FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_NetworkCatchment_CatchmentGeometry] ON [dbo].[NetworkCatchment]
(
	[CatchmentGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]