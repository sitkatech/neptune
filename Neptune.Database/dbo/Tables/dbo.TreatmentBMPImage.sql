CREATE TABLE [dbo].[TreatmentBMPImage](
	[TreatmentBMPImageID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPImage_TreatmentBMPImageID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPImage_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[Caption] [varchar](500) NULL,
	[UploadDate] [date] NOT NULL,
	CONSTRAINT [AK_TreatmentBMPImage_FileResourceID_TreatmentBMPID] UNIQUE([FileResourceID], [TreatmentBMPID])
)