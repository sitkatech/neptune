SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Neighborhood](
	[NeighborhoodID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NeighborhoodGeometry] [geometry] NOT NULL,
	[OCSurveyNeighborhoodID] [int] NOT NULL,
	[OCSurveyDownstreamNeighborhoodID] [int] NULL,
	[NeighborhoodGeometry4326] [geometry] NULL,
 CONSTRAINT [PK_Neighborhood_NeighborhoodID] PRIMARY KEY CLUSTERED 
(
	[NeighborhoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Neighborhood_OCSurveyNeighborhoodID] UNIQUE NONCLUSTERED 
(
	[OCSurveyNeighborhoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Neighborhood]  WITH CHECK ADD  CONSTRAINT [FK_Neighborhood_Neighborhood_OCSurveyDownstreamNeighborhoodID_OCSurveyNeighborhoodID] FOREIGN KEY([OCSurveyDownstreamNeighborhoodID])
REFERENCES [dbo].[Neighborhood] ([OCSurveyNeighborhoodID])
GO
ALTER TABLE [dbo].[Neighborhood] CHECK CONSTRAINT [FK_Neighborhood_Neighborhood_OCSurveyDownstreamNeighborhoodID_OCSurveyNeighborhoodID]