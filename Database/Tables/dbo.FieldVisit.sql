SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldVisit](
	[FieldVisitID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[FieldVisitStatusID] [int] NOT NULL,
	[PerformedByPersonID] [int] NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[InventoryUpdated] [bit] NOT NULL,
	[FieldVisitTypeID] [int] NOT NULL,
	[IsFieldVisitVerified] [bit] NOT NULL,
 CONSTRAINT [PK_FieldVisit_FieldVisitID] PRIMARY KEY CLUSTERED 
(
	[FieldVisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_FieldVisit_FieldVisitID_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[FieldVisitID] ASC,
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX [CK_AtMostOneFieldVisitMayBeInProgressAtAnyTimePerBMP] ON [dbo].[FieldVisit]
(
	[TreatmentBMPID] ASC
)
WHERE ([FieldVisitStatusID]=(1))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_FieldVisitStatus_FieldVisitStatusID] FOREIGN KEY([FieldVisitStatusID])
REFERENCES [dbo].[FieldVisitStatus] ([FieldVisitStatusID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_FieldVisitStatus_FieldVisitStatusID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_FieldVisitType_FieldVisitTypeID] FOREIGN KEY([FieldVisitTypeID])
REFERENCES [dbo].[FieldVisitType] ([FieldVisitTypeID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_FieldVisitType_FieldVisitTypeID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_PersonID] FOREIGN KEY([PerformedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID]