SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecordObservation](
	[MaintenanceRecordObservationID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[MaintenanceRecordID] [int] NOT NULL,
	[TreatmentBMPTypeAttributeTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_MaintenanceRecordObservation_MaintenanceRecordObservationID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceRecordObservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID] FOREIGN KEY([MaintenanceRecordID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TenantID] FOREIGN KEY([MaintenanceRecordID], [TenantID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_Tenant_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID] FOREIGN KEY([TreatmentBMPAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID] FOREIGN KEY([TreatmentBMPAttributeTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAttributeType] ([TreatmentBMPTypeAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeAttributeTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPTypeAttributeType] ([TreatmentBMPTypeAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeID], [TreatmentBMPAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAttributeType] ([TreatmentBMPTypeID], [TreatmentBMPAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID]