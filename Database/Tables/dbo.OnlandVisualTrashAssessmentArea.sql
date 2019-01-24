SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentArea](
	[OnlandVisualTrashAssessmentAreaID] [int] IDENTITY(1,1) NOT NULL,
	[OnlandVisualTrashAssessmentAreaGeometry] [geometry] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
