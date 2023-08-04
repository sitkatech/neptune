CREATE TABLE [dbo].[OnlandVisualTrashAssessmentArea](
	[OnlandVisualTrashAssessmentAreaID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] PRIMARY KEY,
	[OnlandVisualTrashAssessmentAreaName] [varchar](100),
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[OnlandVisualTrashAssessmentAreaGeometry] [geometry] NOT NULL,
	[OnlandVisualTrashAssessmentBaselineScoreID] [int] NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID]),
	[AssessmentAreaDescription] [varchar](500) NULL,
	[TransectLine] [geometry] NULL,
	[OnlandVisualTrashAssessmentProgressScoreID] [int] NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID]),
	[OnlandVisualTrashAssessmentAreaGeometry4326] [geometry] NULL,
	[TransectLine4326] [geometry] NULL,
	CONSTRAINT [AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID] UNIQUE([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID]),
	CONSTRAINT [AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaName_StormwaterJurisdictionID] UNIQUE([OnlandVisualTrashAssessmentAreaName], [StormwaterJurisdictionID])
)
GO

CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry] ON [dbo].[OnlandVisualTrashAssessmentArea]
(
	[OnlandVisualTrashAssessmentAreaGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine] ON [dbo].[OnlandVisualTrashAssessmentArea]
(
	[TransectLine]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)