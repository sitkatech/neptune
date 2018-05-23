SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldVisit](
	[FieldVisitID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[FieldVisitStatusID] [int] NULL,
	[InitialAssessmentID] [int] NULL,
	[MaintenanceRecordID] [int] NULL,
	[PostMaintenanceAssessmentID] [int] NULL,
	[PerformedByPersonID] [int] NOT NULL,
	[VisitDate] [datetime] NOT NULL,
 CONSTRAINT [PK_FieldVisit_FieldVisitID] PRIMARY KEY CLUSTERED 
(
	[FieldVisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID] FOREIGN KEY([PostMaintenanceAssessmentID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TenantID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID]
GO
ALTER TABLE [dbo].[FieldVisit]  WITH CHECK ADD  CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID] FOREIGN KEY([PostMaintenanceAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[FieldVisit] CHECK CONSTRAINT [FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID]