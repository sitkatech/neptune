CREATE TABLE [dbo].[FundingEventFundingSource](
	[FundingEventFundingSourceID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FundingEventFundingSource_FundingEventFundingSourceID] PRIMARY KEY,
	[FundingSourceID] [int] NOT NULL CONSTRAINT [FK_FundingEventFundingSource_FundingSource_FundingSourceID] FOREIGN KEY REFERENCES [dbo].[FundingSource] ([FundingSourceID]),
	[FundingEventID] [int] NOT NULL CONSTRAINT [FK_FundingEventFundingSource_FundingEvent_FundingEventID] FOREIGN KEY REFERENCES [dbo].[FundingEvent] ([FundingEventID]),
	[Amount] [money] NULL,
	CONSTRAINT [AK_FundingEventFundingSource_FundingSourceID_FundingEventID] UNIQUE([FundingSourceID], [FundingEventID])
)