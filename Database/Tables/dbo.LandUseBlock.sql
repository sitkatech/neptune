SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandUseBlock](
	[LandUseBlockID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandUseTypeID] [int] NULL,
	[LandUseDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUseBlockGeometry] [geometry] NOT NULL,
 CONSTRAINT [PK_LandUseBlock_LandUseBlockID] PRIMARY KEY CLUSTERED 
(
	[LandUseBlockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[LandUseBlock]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID] FOREIGN KEY([PriorityLandUseTypeID])
REFERENCES [dbo].[PriorityLandUseType] ([PriorityLandUseTypeID])
GO
ALTER TABLE [dbo].[LandUseBlock] CHECK CONSTRAINT [FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID]