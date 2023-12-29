CREATE TABLE [dbo].[MaintenanceRecord](
	[MaintenanceRecordID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_MaintenanceRecord_MaintenanceRecordID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_MaintenanceRecord_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[FieldVisitID] [int] NOT NULL CONSTRAINT [AK_MaintenanceRecord_FieldVisitID] UNIQUE CONSTRAINT [FK_MaintenanceRecord_FieldVisit_FieldVisitID] FOREIGN KEY REFERENCES [dbo].[FieldVisit] ([FieldVisitID]),
	[MaintenanceRecordDescription] [varchar](500) NULL,
	[MaintenanceRecordTypeID] [int] NULL CONSTRAINT [FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID] FOREIGN KEY REFERENCES [dbo].[MaintenanceRecordType] ([MaintenanceRecordTypeID]),
	CONSTRAINT [AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID] UNIQUE([MaintenanceRecordID], [TreatmentBMPID]),
	CONSTRAINT [AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID] UNIQUE([MaintenanceRecordID], [TreatmentBMPTypeID]),
	--CONSTRAINT [FK_MaintenanceRecord_FieldVisit_FieldVisitID_TreatmentBMPID] FOREIGN KEY([FieldVisitID], [TreatmentBMPID]) REFERENCES [dbo].[FieldVisit] ([FieldVisitID], [TreatmentBMPID]),
	--CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID]) REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
)