CREATE TABLE [dbo].[OnlandVisualTrashAssessmentScore](
	[OnlandVisualTrashAssessmentScoreID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentScoreName] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OnlandVisualTrashAssessmentScoreDisplayName] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NumericValue] [int] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreDisplayName] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentScoreDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreName] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentScoreName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
