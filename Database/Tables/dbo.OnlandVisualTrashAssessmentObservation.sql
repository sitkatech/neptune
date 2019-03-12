SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservation](
	[OnlandVisualTrashAssessmentObservationID] [int] IDENTITY(1,1) NOT NULL,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL,
	[LocationPoint] [geometry] NOT NULL,
	[Note] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ObservationDatetime] [datetime] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentObservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservation]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY([OnlandVisualTrashAssessmentID])
REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservation] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservation]  WITH CHECK ADD  CONSTRAINT [CK_LocationIsAPoint] CHECK  (([LocationPoint].[STGeometryType]()='Point'))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservation] CHECK CONSTRAINT [CK_LocationIsAPoint]