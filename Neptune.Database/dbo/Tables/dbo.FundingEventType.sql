CREATE TABLE [dbo].[FundingEventType](
	[FundingEventTypeID] [int] NOT NULL CONSTRAINT [PK_FundingEventType_FundingEventTypeID] PRIMARY KEY,
	[FundingEventTypeName] [varchar](100) CONSTRAINT [AK_FundingEventType_FundingEventTypeName] UNIQUE,
	[FundingEventTypeDisplayName] [varchar](100) CONSTRAINT [AK_FundingEventType_FundingEventTypeDisplayName] UNIQUE
)