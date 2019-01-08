SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecordObservation](
	[MaintenanceRecordObservationID] [int] IDENTITY(1,1) NOT NULL,
	[MaintenanceRecordID] [int] NOT NULL,
	[TreatmentBMPTypeCustomAttributeTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[CustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_MaintenanceRecordObservation_MaintenanceRecordObservationID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceRecordObservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY([CustomAttributeTypeID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_CustomAttributeType_CustomAttributeTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID] FOREIGN KEY([MaintenanceRecordID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID] FOREIGN KEY([MaintenanceRecordID], [TreatmentBMPTypeID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecordID_TreatmentBMPTypeID] FOREIGN KEY([MaintenanceRecordID], [TreatmentBMPTypeID])
REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_MaintenanceRecordID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeCustomAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeID], [CustomAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeID], [CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservation] CHECK CONSTRAINT [FK_MaintenanceRecordObservation_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID]