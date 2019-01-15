SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessment](
	[OnlandVisualTrashAssessmentID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID]