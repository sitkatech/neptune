CREATE TABLE [dbo].[FundingEvent](
	[FundingEventID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[FundingEventTypeID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Description] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_FundingEvent_FundingEventID] PRIMARY KEY CLUSTERED 
(
	[FundingEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_FundingEventType_FundingEventTypeID] FOREIGN KEY([FundingEventTypeID])
REFERENCES [dbo].[FundingEventType] ([FundingEventTypeID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_FundingEventType_FundingEventTypeID]
GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID]