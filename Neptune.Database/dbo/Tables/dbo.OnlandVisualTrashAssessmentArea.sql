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

create spatial index [SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry] on [dbo].[OnlandVisualTrashAssessmentArea]
(
	[OnlandVisualTrashAssessmentAreaGeometry]
)
with (BOUNDING_BOX=(1.8348e+006, 644073, 1.87062e+006, 690279))
GO

create spatial index [SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry4326] on [dbo].[OnlandVisualTrashAssessmentArea]
(
	[OnlandVisualTrashAssessmentAreaGeometry4326]
)
with (BOUNDING_BOX=(-119, 33, -117, 34))
GO

create spatial index [SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine] on [dbo].[OnlandVisualTrashAssessmentArea]
(
	[TransectLine]
)
with (BOUNDING_BOX=(1.83482e+006, 644249, 1.87056e+006, 690269))
GO

create spatial index [SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine4326] on [dbo].[OnlandVisualTrashAssessmentArea]
(
	[TransectLine4326]
)
with (BOUNDING_BOX=(-119, 33, -117, 34))
GO