CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging](
	[OnlandVisualTrashAssessmentObservationPhotoStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessmentObservationPhotoStagingID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[OnlandVisualTrashAssessmentID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID])
)