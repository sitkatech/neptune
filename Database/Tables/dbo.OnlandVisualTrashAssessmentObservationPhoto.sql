SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto](
	[OnlandVisualTrashAssessmentObservationPhotoID] [int] IDENTITY(1,1) NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentObservationID] [int] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservationPhotoID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentObservationPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID] FOREIGN KEY([OnlandVisualTrashAssessmentObservationID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentObservation] ([OnlandVisualTrashAssessmentObservationID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID]