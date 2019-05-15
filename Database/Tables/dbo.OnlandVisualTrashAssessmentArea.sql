SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentArea](
	[OnlandVisualTrashAssessmentAreaID] [int] IDENTITY(1,1) NOT NULL,
	[OnlandVisualTrashAssessmentAreaName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentAreaGeometry] [geometry] NOT NULL,
	[OnlandVisualTrashAssessmentBaselineScoreID] [int] NULL,
	[AssessmentAreaDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TransectLine] [geometry] NULL,
	[OnlandVisualTrashAssessmentProgressScoreID] [int] NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentAreaID] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaName_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentAreaName] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash] FOREIGN KEY([OnlandVisualTrashAssessmentBaselineScoreID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash] FOREIGN KEY([OnlandVisualTrashAssessmentProgressScoreID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentArea] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry] ON [dbo].[OnlandVisualTrashAssessmentArea]
(
	[OnlandVisualTrashAssessmentAreaGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]