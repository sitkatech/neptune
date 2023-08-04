CREATE TABLE [dbo].[OnlandVisualTrashAssessmentScore](
	[OnlandVisualTrashAssessmentScoreID] [int] NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID] PRIMARY KEY,
	[OnlandVisualTrashAssessmentScoreName] [varchar](100) CONSTRAINT [AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreName] UNIQUE,
	[OnlandVisualTrashAssessmentScoreDisplayName] [varchar](100) CONSTRAINT [AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreDisplayName] UNIQUE,
	[NumericValue] [int] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NOT NULL
)
