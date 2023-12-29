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
GO

CREATE SPATIAL INDEX [SPATIAL_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestGeometry] ON [dbo].[RegionalSubbasinRevisionRequest]
(
	[RegionalSubbasinRevisionRequestGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)