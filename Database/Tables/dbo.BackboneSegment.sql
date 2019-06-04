SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BackboneSegment](
	[BackboneSegmentID] [int] IDENTITY(1,1) NOT NULL,
	[BackboneSegmentGeometry] [geometry] NOT NULL,
	[BackboneSegmentAlternateID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DownstreamBackboneSegmentAlternateID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CatchIDN] [int] NOT NULL,
	[NetworkCatchmentID] [int] NULL,
	[BackboneSegmentTypeID] [int] NOT NULL,
 CONSTRAINT [PK_BackboneSegment_BackboneSegmentID] PRIMARY KEY CLUSTERED 
(
	[BackboneSegmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_BackboneSegment_BackboneSegmentAlternateID] UNIQUE NONCLUSTERED 
(
	[BackboneSegmentAlternateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[BackboneSegment]  WITH CHECK ADD  CONSTRAINT [FK_BackboneSegment_BackboneSegmentType_BackboneSegmentTypeID] FOREIGN KEY([BackboneSegmentTypeID])
REFERENCES [dbo].[BackboneSegmentType] ([BackboneSegmentTypeID])
GO
ALTER TABLE [dbo].[BackboneSegment] CHECK CONSTRAINT [FK_BackboneSegment_BackboneSegmentType_BackboneSegmentTypeID]
GO
ALTER TABLE [dbo].[BackboneSegment]  WITH CHECK ADD  CONSTRAINT [FK_BackboneSegment_NetworkCatchment_NetworkCatchmentID] FOREIGN KEY([NetworkCatchmentID])
REFERENCES [dbo].[NetworkCatchment] ([NetworkCatchmentID])
GO
ALTER TABLE [dbo].[BackboneSegment] CHECK CONSTRAINT [FK_BackboneSegment_NetworkCatchment_NetworkCatchmentID]