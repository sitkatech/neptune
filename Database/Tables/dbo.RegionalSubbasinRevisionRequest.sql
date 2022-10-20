SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegionalSubbasinRevisionRequest](
	[RegionalSubbasinRevisionRequestID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[RegionalSubbasinRevisionRequestGeometry] [geometry] NOT NULL,
	[RequestPersonID] [int] NOT NULL,
	[RegionalSubbasinRevisionRequestStatusID] [int] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[ClosedByPersonID] [int] NULL,
	[ClosedDate] [datetime] NULL,
	[Notes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CloseNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestID] PRIMARY KEY CLUSTERED 
(
	[RegionalSubbasinRevisionRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID] FOREIGN KEY([ClosedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest] CHECK CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID] FOREIGN KEY([RequestPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest] CHECK CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID]
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID] FOREIGN KEY([RegionalSubbasinRevisionRequestStatusID])
REFERENCES [dbo].[RegionalSubbasinRevisionRequestStatus] ([RegionalSubbasinRevisionRequestStatusID])
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest] CHECK CONSTRAINT [FK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID]
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest]  WITH CHECK ADD  CONSTRAINT [FK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest] CHECK CONSTRAINT [FK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest]  WITH CHECK ADD  CONSTRAINT [CK_RegionalSubbasinRevisionRequest_ClosedReqMustHaveCloseDate] CHECK  ((NOT ([RegionalSubbasinRevisionRequestStatusID]=(2) AND [ClosedDate] IS NULL)))
GO
ALTER TABLE [dbo].[RegionalSubbasinRevisionRequest] CHECK CONSTRAINT [CK_RegionalSubbasinRevisionRequest_ClosedReqMustHaveCloseDate]