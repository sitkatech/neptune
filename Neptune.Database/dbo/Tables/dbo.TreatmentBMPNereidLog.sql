CREATE TABLE [dbo].[TreatmentBMPNereidLog](
	[TreatmentBMPNereidLogID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPNereidLog_TreatmentBMPNereidLogID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPNereidLog_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[LastRequestDate] DATETIME NULL,
    [NereidRequest] VARCHAR(MAX) NULL,
    [NereidResponse] VARCHAR(MAX) NULL,
	CONSTRAINT [AK_TreatmentBMPNereidLog_TreatmentBMPID] UNIQUE([TreatmentBMPID])
)