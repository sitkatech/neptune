SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldVisit](
	[FieldVisitID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[FieldVisitStatusID] [int] NOT NULL,
	[InitialAssessmentID] [int] NULL,
	[MaintenanceRecordID] [int] NULL,
	[PostMaintenanceAssessmentID] [int] NULL,
	[PerformedByPersonID] [int] NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[InventoryUpdated] [bit] NOT NULL,
 CONSTRAINT [PK_FieldVisit_FieldVisitID] PRIMARY KEY CLUSTERED 
(
	[FieldVisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_FieldVisit_InitialAssessmentID] ON [dbo].[FieldVisit]
(
	[InitialAssessmentID] ASC
)
WHERE ([InitialAssessmentID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_FieldVisit_MaintenanceRecordID] ON [dbo].[FieldVisit]
(
	[MaintenanceRecordID] ASC
)
WHERE ([MaintenanceRecordID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_FieldVisit_PostMaintenanceAssessmentID] ON [dbo].[FieldVisit]
(
	[PostMaintenanceAssessmentID] ASC
)
WHERE ([PostMaintenanceAssessmentID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID] FOREIGN KEY([MaintenanceRecordID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TenantID] FOREIGN KEY([MaintenanceRecordID], [TenantID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID] FOREIGN KEY([MaintenanceRecordID], [TreatmentBMPID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID], [TreatmentBMPID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_PersonID] FOREIGN KEY([PerformedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_TenantID_PersonID_TenantID] FOREIGN KEY([PerformedByPersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_Person_PerformedByPersonID_TenantID_PersonID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_Tenant_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMP_TreatmentBMPID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID] FOREIGN KEY([InitialAssessmentID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPAssessmentID] FOREIGN KEY([InitialAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPAssessmentID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID] FOREIGN KEY([InitialAssessmentID], [TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TreatmentBMPID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID] FOREIGN KEY([PostMaintenanceAssessmentID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID] FOREIGN KEY([PostMaintenanceAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID] FOREIGN KEY([PostMaintenanceAssessmentID], [TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TreatmentBMPID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [CK_InitialAssessmentMustBeDifferentFromPMAssessmentIfNotBothNull] CHECK  (([InitialAssessmentID]<>[PostMaintenanceAssessmentID] OR [InitialAssessmentID] IS NULL AND [PostMaintenanceAssessmentID] IS NULL))
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [CK_InitialAssessmentMustBeDifferentFromPMAssessmentIfNotBothNull]