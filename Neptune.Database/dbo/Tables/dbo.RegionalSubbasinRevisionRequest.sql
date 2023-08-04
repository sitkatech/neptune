CREATE TABLE [dbo].[RegionalSubbasinRevisionRequest](
	[RegionalSubbasinRevisionRequestID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[RegionalSubbasinRevisionRequestGeometry] [geometry] NOT NULL,
	[RequestPersonID] [int] NOT NULL CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[RegionalSubbasinRevisionRequestStatusID] [int] NOT NULL CONSTRAINT [FK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasinRevisionRequestStatus] ([RegionalSubbasinRevisionRequestStatusID]),
	[RequestDate] [datetime] NOT NULL,
	[ClosedByPersonID] [int] NULL CONSTRAINT [FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[ClosedDate] [datetime] NULL,
	[Notes] [varchar](max) NULL,
	[CloseNotes] [varchar](max) NULL,
	CONSTRAINT [CK_RegionalSubbasinRevisionRequest_ClosedReqMustHaveCloseDate] CHECK  ((NOT ([RegionalSubbasinRevisionRequestStatusID]=(2) AND [ClosedDate] IS NULL)))
)