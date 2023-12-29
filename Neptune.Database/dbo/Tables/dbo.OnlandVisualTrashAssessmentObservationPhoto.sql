CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhoto](
	[OnlandVisualTrashAssessmentObservationPhotoID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservationPhotoID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[OnlandVisualTrashAssessmentObservationID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentObservation] ([OnlandVisualTrashAssessmentObservationID])
)