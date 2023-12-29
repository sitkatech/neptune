CREATE TABLE [dbo].[FieldVisit](
	[FieldVisitID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FieldVisit_FieldVisitID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[FieldVisitStatusID] [int] NOT NULL CONSTRAINT [FK_FieldVisit_FieldVisitStatus_FieldVisitStatusID] FOREIGN KEY REFERENCES [dbo].[FieldVisitStatus] ([FieldVisitStatusID]),
	[PerformedByPersonID] [int] NOT NULL CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[VisitDate] [datetime] NOT NULL,
	[InventoryUpdated] [bit] NOT NULL,
	[FieldVisitTypeID] [int] NOT NULL CONSTRAINT [FK_FieldVisit_FieldVisitType_FieldVisitTypeID] FOREIGN KEY REFERENCES [dbo].[FieldVisitType] ([FieldVisitTypeID]),
	[IsFieldVisitVerified] [bit] NOT NULL,
	--CONSTRAINT [AK_FieldVisit_FieldVisitID_TreatmentBMPID] UNIQUE ([FieldVisitID], [TreatmentBMPID])
)

GO
CREATE UNIQUE NONCLUSTERED INDEX [CK_AtMostOneFieldVisitMayBeInProgressAtAnyTimePerBMP] ON [dbo].[FieldVisit]
(
	[TreatmentBMPID] ASC
)
WHERE ([FieldVisitStatusID]=(1))