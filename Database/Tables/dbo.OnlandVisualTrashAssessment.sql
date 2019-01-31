SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessment](
	[OnlandVisualTrashAssessmentID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[Notes] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StormwaterJurisdictionID] [int] NULL,
	[AssessingNewArea] [bit] NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID]