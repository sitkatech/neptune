/*
   Monday, September 17, 201811:07:50 AM
   User: 
   Server: (local)
   Database: Neptune
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_FieldVisit_FieldVisitID
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_FieldVisit_FieldVisitID_TreatmentBMPID
GO
ALTER TABLE dbo.FieldVisit SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID
GO
ALTER TABLE dbo.MaintenanceRecordType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TenantID
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMP SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MaintenanceRecord
	DROP CONSTRAINT FK_MaintenanceRecord_Tenant_TenantID
GO
ALTER TABLE dbo.Tenant SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_MaintenanceRecord
	(
	MaintenanceRecordID int NOT NULL IDENTITY (1, 1),
	TenantID int NOT NULL,
	TreatmentBMPID int NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	FieldVisitID int NOT NULL,
	MaintenanceRecordDescription varchar(500) NULL,
	MaintenanceRecordTypeID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_MaintenanceRecord SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_MaintenanceRecord ON
GO
IF EXISTS(SELECT * FROM dbo.MaintenanceRecord)
	 EXEC('INSERT INTO dbo.Tmp_MaintenanceRecord (MaintenanceRecordID, TenantID, TreatmentBMPID, TreatmentBMPTypeID, FieldVisitID, MaintenanceRecordDescription, MaintenanceRecordTypeID)
		SELECT MaintenanceRecordID, TenantID, TreatmentBMPID, TreatmentBMPTypeID, FieldVisitID, MaintenanceRecordDescription, MaintenanceRecordTypeID FROM dbo.MaintenanceRecord WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_MaintenanceRecord OFF
GO
ALTER TABLE dbo.MaintenanceRecordObservation
	DROP CONSTRAINT FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID
GO
ALTER TABLE dbo.MaintenanceRecordObservation
	DROP CONSTRAINT FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TenantID
GO
ALTER TABLE dbo.MaintenanceRecordObservation
	DROP CONSTRAINT FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.MaintenanceRecordObservation
	DROP CONSTRAINT FK_MaintenanceRecordObservation_MaintenanceRecordID_TreatmentBMPTypeID
GO
DROP TABLE dbo.MaintenanceRecord
GO
EXECUTE sp_rename N'dbo.Tmp_MaintenanceRecord', N'MaintenanceRecord', 'OBJECT' 
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	PK_MaintenanceRecord_MaintenanceRecordID PRIMARY KEY CLUSTERED 
	(
	MaintenanceRecordID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	AK_MaintenanceRecord_MaintenanceRecordID_TenantID UNIQUE NONCLUSTERED 
	(
	MaintenanceRecordID,
	TenantID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID UNIQUE NONCLUSTERED 
	(
	MaintenanceRecordID,
	TreatmentBMPTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID UNIQUE NONCLUSTERED 
	(
	MaintenanceRecordID,
	TreatmentBMPID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_Tenant_TenantID FOREIGN KEY
	(
	TenantID
	) REFERENCES dbo.Tenant
	(
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID FOREIGN KEY
	(
	TreatmentBMPID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID FOREIGN KEY
	(
	MaintenanceRecordTypeID
	) REFERENCES dbo.MaintenanceRecordType
	(
	MaintenanceRecordTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY
	(
	TreatmentBMPID,
	TenantID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_FieldVisit_FieldVisitID FOREIGN KEY
	(
	FieldVisitID
	) REFERENCES dbo.FieldVisit
	(
	FieldVisitID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_FieldVisit_FieldVisitID_TreatmentBMPID FOREIGN KEY
	(
	FieldVisitID,
	TreatmentBMPID
	) REFERENCES dbo.FieldVisit
	(
	FieldVisitID,
	TreatmentBMPID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecord ADD CONSTRAINT
	FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID FOREIGN KEY
	(
	TreatmentBMPID,
	TreatmentBMPTypeID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID,
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MaintenanceRecordObservation ADD CONSTRAINT
	FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID FOREIGN KEY
	(
	MaintenanceRecordID
	) REFERENCES dbo.MaintenanceRecord
	(
	MaintenanceRecordID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecordObservation ADD CONSTRAINT
	FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TenantID FOREIGN KEY
	(
	MaintenanceRecordID,
	TenantID
	) REFERENCES dbo.MaintenanceRecord
	(
	MaintenanceRecordID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecordObservation ADD CONSTRAINT
	FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID FOREIGN KEY
	(
	MaintenanceRecordID,
	TreatmentBMPTypeID
	) REFERENCES dbo.MaintenanceRecord
	(
	MaintenanceRecordID,
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecordObservation ADD CONSTRAINT
	FK_MaintenanceRecordObservation_MaintenanceRecordID_TreatmentBMPTypeID FOREIGN KEY
	(
	MaintenanceRecordID,
	TreatmentBMPTypeID
	) REFERENCES dbo.MaintenanceRecord
	(
	MaintenanceRecordID,
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MaintenanceRecordObservation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
