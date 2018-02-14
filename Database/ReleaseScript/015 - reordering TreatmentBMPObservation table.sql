/*
   Tuesday, February 13, 20181:26:16 AM
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
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_Tenant_TenantID
GO
ALTER TABLE dbo.Tenant SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_ObservationTypeI
GO
ALTER TABLE dbo.TreatmentBMPTypeObservationType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_ObservationType_ObservationTypeID
GO
ALTER TABLE dbo.ObservationType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID
GO
ALTER TABLE dbo.TreatmentBMPObservation
	DROP CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMPAssessment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_TreatmentBMPObservation
	(
	TreatmentBMPObservationID int NOT NULL IDENTITY (1, 1),
	TenantID int NOT NULL,
	TreatmentBMPAssessmentID int NOT NULL,
	TreatmentBMPTypeObservationTypeID int NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	ObservationTypeID int NOT NULL,
	ObservationData nvarchar(MAX) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TreatmentBMPObservation SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPObservation ON
GO
IF EXISTS(SELECT * FROM dbo.TreatmentBMPObservation)
	 EXEC('INSERT INTO dbo.Tmp_TreatmentBMPObservation (TreatmentBMPObservationID, TenantID, TreatmentBMPAssessmentID, TreatmentBMPTypeObservationTypeID, TreatmentBMPTypeID, ObservationTypeID, ObservationData)
		SELECT TreatmentBMPObservationID, TenantID, TreatmentBMPAssessmentID, TreatmentBMPTypeObservationTypeID, TreatmentBMPTypeID, ObservationTypeID, ObservationData FROM dbo.TreatmentBMPObservation WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPObservation OFF
GO
DROP TABLE dbo.TreatmentBMPObservation
GO
EXECUTE sp_rename N'dbo.Tmp_TreatmentBMPObservation', N'TreatmentBMPObservation', 'OBJECT' 
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	PK_TreatmentBMPObservation_TreatmentBMPObservationID PRIMARY KEY CLUSTERED 
	(
	TreatmentBMPObservationID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID FOREIGN KEY
	(
	TreatmentBMPAssessmentID
	) REFERENCES dbo.TreatmentBMPAssessment
	(
	TreatmentBMPAssessmentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_ObservationType_ObservationTypeID FOREIGN KEY
	(
	ObservationTypeID
	) REFERENCES dbo.ObservationType
	(
	ObservationTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID FOREIGN KEY
	(
	TreatmentBMPAssessmentID,
	TenantID
	) REFERENCES dbo.TreatmentBMPAssessment
	(
	TreatmentBMPAssessmentID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID FOREIGN KEY
	(
	TreatmentBMPAssessmentID,
	TreatmentBMPTypeID
	) REFERENCES dbo.TreatmentBMPAssessment
	(
	TreatmentBMPAssessmentID,
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID FOREIGN KEY
	(
	TreatmentBMPTypeObservationTypeID
	) REFERENCES dbo.TreatmentBMPTypeObservationType
	(
	TreatmentBMPTypeObservationTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_ObservationTypeI FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT
	FK_TreatmentBMPObservation_Tenant_TenantID FOREIGN KEY
	(
	TenantID
	) REFERENCES dbo.Tenant
	(
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 

alter table dbo.TreatmentBMPObservation add constraint FK_TreatmentBMPObservation_TreatmentBMPType_TreatmentBMPTypeID foreign key (TreatmentBMPTypeID) references dbo.TreatmentBMPType(TreatmentBMPTypeID)  ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
