SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentStatus](
	[OnlandVisualTrashAssessmentStatusID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentStatusName] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OnlandVisualTrashAssessmentStatusDisplayName] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusDisplayName] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentStatusDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusName] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
