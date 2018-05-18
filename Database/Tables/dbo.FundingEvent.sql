SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FundingEvent](
	[FundingEventID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[FundingEventTypeID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Description] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_FundingEvent_FundingEventID] PRIMARY KEY CLUSTERED 
(
	[FundingEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_FundingEvent_FundingEventID_TenantID] UNIQUE NONCLUSTERED 
(
	[FundingEventID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_FundingEventType_FundingEventTypeID] FOREIGN KEY([FundingEventTypeID])
REFERENCES [dbo].[FundingEventType] ([FundingEventTypeID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_FundingEventType_FundingEventTypeID]
GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_Tenant_TenantID]
GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[FundingEvent]  WITH CHECK ADD  CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[FundingEvent] CHECK CONSTRAINT [FK_FundingEvent_TreatmentBMP_TreatmentBMPID_TenantID]