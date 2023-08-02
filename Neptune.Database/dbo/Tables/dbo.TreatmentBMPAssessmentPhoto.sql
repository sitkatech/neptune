CREATE TABLE [dbo].[TreatmentBMPAssessmentPhoto](
	[TreatmentBMPAssessmentPhotoID] [int] IDENTITY(1,1) NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[TreatmentBMPAssessmentID] [int] NOT NULL,
	[Caption] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessmentPhotoID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAssessmentPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID] FOREIGN KEY([TreatmentBMPAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID]