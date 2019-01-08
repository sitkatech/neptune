SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FundingEventFundingSource](
	[FundingEventFundingSourceID] [int] IDENTITY(1,1) NOT NULL,
	[FundingSourceID] [int] NOT NULL,
	[FundingEventID] [int] NOT NULL,
	[Amount] [money] NULL,
 CONSTRAINT [PK_FundingEventFundingSource_FundingEventFundingSourceID] PRIMARY KEY CLUSTERED 
(
	[FundingEventFundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_FundingEventFundingSource_FundingSourceID_FundingEventID] UNIQUE NONCLUSTERED 
(
	[FundingSourceID] ASC,
	[FundingEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FundingEventFundingSource]  WITH CHECK ADD  CONSTRAINT [FK_FundingEventFundingSource_FundingEvent_FundingEventID] FOREIGN KEY([FundingEventID])
REFERENCES [dbo].[FundingEvent] ([FundingEventID])
GO
ALTER TABLE [dbo].[FundingEventFundingSource] CHECK CONSTRAINT [FK_FundingEventFundingSource_FundingEvent_FundingEventID]
GO
ALTER TABLE [dbo].[FundingEventFundingSource]  WITH CHECK ADD  CONSTRAINT [FK_FundingEventFundingSource_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[FundingEventFundingSource] CHECK CONSTRAINT [FK_FundingEventFundingSource_FundingSource_FundingSourceID]