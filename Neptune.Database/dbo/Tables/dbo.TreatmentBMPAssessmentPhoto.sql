CREATE TABLE [dbo].[TreatmentBMPAssessmentPhoto](
	[TreatmentBMPAssessmentPhotoID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessmentPhotoID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[TreatmentBMPAssessmentID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID]),
	[Caption] [varchar](500) NULL
)