/*
   Tuesday, February 13, 20181:20:35 AM
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
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_Tenant_TenantID
GO
ALTER TABLE dbo.Tenant SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_Observ
GO
ALTER TABLE dbo.TreatmentBMPTypeObservationType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMPType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TenantID
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMP SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold
	DROP CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_ObservationType_ObservationTypeID
GO
ALTER TABLE dbo.ObservationType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_TreatmentBMPBenchmarkAndThreshold
	(
	TreatmentBMPBenchmarkAndThresholdID int NOT NULL IDENTITY (1, 1),
	TenantID int NOT NULL,
	TreatmentBMPID int NOT NULL,
	TreatmentBMPTypeObservationTypeID int NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	ObservationTypeID int NOT NULL,
	BenchmarkValue float(53) NOT NULL,
	ThresholdValue float(53) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TreatmentBMPBenchmarkAndThreshold SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPBenchmarkAndThreshold ON
GO
IF EXISTS(SELECT * FROM dbo.TreatmentBMPBenchmarkAndThreshold)
	 EXEC('INSERT INTO dbo.Tmp_TreatmentBMPBenchmarkAndThreshold (TreatmentBMPBenchmarkAndThresholdID, TenantID, TreatmentBMPID, TreatmentBMPTypeObservationTypeID, TreatmentBMPTypeID, ObservationTypeID, BenchmarkValue, ThresholdValue)
		SELECT TreatmentBMPBenchmarkAndThresholdID, TenantID, TreatmentBMPID, TreatmentBMPTypeObservationTypeID, TreatmentBMPTypeID, ObservationTypeID, BenchmarkValue, ThresholdValue FROM dbo.TreatmentBMPBenchmarkAndThreshold WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPBenchmarkAndThreshold OFF
GO
DROP TABLE dbo.TreatmentBMPBenchmarkAndThreshold
GO
EXECUTE sp_rename N'dbo.Tmp_TreatmentBMPBenchmarkAndThreshold', N'TreatmentBMPBenchmarkAndThreshold', 'OBJECT' 
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	PK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID PRIMARY KEY CLUSTERED 
	(
	TreatmentBMPBenchmarkAndThresholdID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_ObservationTypeID UNIQUE NONCLUSTERED 
	(
	TreatmentBMPID,
	ObservationTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_ObservationType_ObservationTypeID FOREIGN KEY
	(
	ObservationTypeID
	) REFERENCES dbo.ObservationType
	(
	ObservationTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID FOREIGN KEY
	(
	TreatmentBMPID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY
	(
	TreatmentBMPTypeID
	) REFERENCES dbo.TreatmentBMPType
	(
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID FOREIGN KEY
	(
	TreatmentBMPTypeObservationTypeID
	) REFERENCES dbo.TreatmentBMPTypeObservationType
	(
	TreatmentBMPTypeObservationTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_Observ FOREIGN KEY
	(
	TreatmentBMPTypeObservationTypeID,
	TreatmentBMPTypeID,
	ObservationTypeID
	) REFERENCES dbo.TreatmentBMPTypeObservationType
	(
	TreatmentBMPTypeObservationTypeID,
	TreatmentBMPTypeID,
	ObservationTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT
	FK_TreatmentBMPBenchmarkAndThreshold_Tenant_TenantID FOREIGN KEY
	(
	TenantID
	) REFERENCES dbo.Tenant
	(
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
