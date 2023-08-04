CREATE TABLE [dbo].[FundingEvent](
	[FundingEventID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FundingEvent_FundingEventID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[FundingEventTypeID] [int] NOT NULL CONSTRAINT [FK_FundingEvent_FundingEventType_FundingEventTypeID] FOREIGN KEY REFERENCES [dbo].[FundingEventType] ([FundingEventTypeID]),
	[Year] [int] NOT NULL,
	[Description] [varchar](500) NULL
)