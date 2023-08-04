CREATE TABLE [dbo].[OnlandVisualTrashAssessmentStatus](
	[OnlandVisualTrashAssessmentStatusID] [int] NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID] PRIMARY KEY,
	[OnlandVisualTrashAssessmentStatusName] [varchar](100) CONSTRAINT [AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusName] UNIQUE,
	[OnlandVisualTrashAssessmentStatusDisplayName] [varchar](100) CONSTRAINT [AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusDisplayName] UNIQUE
)
