CREATE TABLE [dbo].[MaintenanceRecordObservationValue](
	[MaintenanceRecordObservationValueID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_MaintenanceRecordObservationValue_MaintenanceRecordObservationValueID] PRIMARY KEY,
	[MaintenanceRecordObservationID] [int] NOT NULL CONSTRAINT [FK_MaintenanceRecordObservationValue_MaintenanceRecordObservation_MaintenanceRecordObservationID] FOREIGN KEY REFERENCES [dbo].[MaintenanceRecordObservation] ([MaintenanceRecordObservationID]),
	[ObservationValue] [varchar](1000),
)