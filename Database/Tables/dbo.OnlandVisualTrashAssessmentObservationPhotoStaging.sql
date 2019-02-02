SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging](
	[OnlandVisualTrashAssessmentObservationPhotoStagingID] [int] IDENTITY(1,1) NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessmentObservationPhotoStagingID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentObservationPhotoStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY([OnlandVisualTrashAssessmentID])
REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID]