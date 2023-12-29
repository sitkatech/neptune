CREATE TABLE [dbo].[TreatmentBMPDocument](
	[TreatmentBMPDocumentID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPDocument_TreatmentBMPDocumentID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPDocument_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPDocument_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[DisplayName] [varchar](200),
	[UploadDate] [date] NOT NULL,
	[DocumentDescription] [varchar](500) NULL,
	CONSTRAINT [AK_TreatmentBMPDocument_FileResourceID_TreatmentBMPID] UNIQUE ([FileResourceID], [TreatmentBMPID])
)