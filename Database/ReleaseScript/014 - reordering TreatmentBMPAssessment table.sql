/*
   Tuesday, February 13, 20181:22:17 AM
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
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_Tenant_TenantID
GO
ALTER TABLE dbo.Tenant SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TenantID
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMP SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_StormwaterAssessmentType_StormwaterAssessmentTypeID
GO
ALTER TABLE dbo.StormwaterAssessmentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_Person_PersonID
GO
ALTER TABLE dbo.TreatmentBMPAssessment
	DROP CONSTRAINT FK_TreatmentBMPAssessment_Person_PersonID_TenantID
GO
ALTER TABLE dbo.Person SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_TreatmentBMPAssessment
	(
	TreatmentBMPAssessmentID int NOT NULL IDENTITY (1, 1),
	TenantID int NOT NULL,
	TreatmentBMPID int NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	StormwaterAssessmentTypeID int NOT NULL,
	AssessmentDate datetime NOT NULL,
	PersonID int NOT NULL,
	AlternateAssessmentScore float(53) NULL,
	AlternateAssessmentRationale varchar(1000) NULL,
	IsPrivate bit NOT NULL,
	Notes varchar(1000) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TreatmentBMPAssessment SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPAssessment ON
GO
IF EXISTS(SELECT * FROM dbo.TreatmentBMPAssessment)
	 EXEC('INSERT INTO dbo.Tmp_TreatmentBMPAssessment (TreatmentBMPAssessmentID, TenantID, TreatmentBMPID, TreatmentBMPTypeID, StormwaterAssessmentTypeID, AssessmentDate, PersonID, AlternateAssessmentScore, AlternateAssessmentRationale, IsPrivate, Notes)
		SELECT TreatmentBMPAssessmentID, TenantID, TreatmentBMPID, TreatmentBMPTypeID, StormwaterAssessmentTypeID, AssessmentDate, PersonID, AlternateAssessmentScore, AlternateAssessmentRationale, IsPrivate, Notes FROM dbo.TreatmentBMPAssessment WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPAssessment OFF
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
DROP TABLE dbo.TreatmentBMPAssessment
GO
EXECUTE sp_rename N'dbo.Tmp_TreatmentBMPAssessment', N'TreatmentBMPAssessment', 'OBJECT' 
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	PK_TreatmentBMPAssessment_TreatmentBMPAssessmentID PRIMARY KEY CLUSTERED 
	(
	TreatmentBMPAssessmentID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID UNIQUE NONCLUSTERED 
	(
	TreatmentBMPAssessmentID,
	TenantID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID UNIQUE NONCLUSTERED 
	(
	TreatmentBMPAssessmentID,
	TreatmentBMPTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	CK_TreatmentBMPAssessment_AlternateAssessmentScoreAndAlternateAssessmentRationaleBothOrNone CHECK (([AlternateAssessmentScore] IS NULL AND [AlternateAssessmentRationale] IS NULL OR [AlternateAssessmentScore] IS NOT NULL AND [AlternateAssessmentRationale] IS NOT NULL))
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_Person_PersonID FOREIGN KEY
	(
	PersonID
	) REFERENCES dbo.Person
	(
	PersonID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_Person_PersonID_TenantID FOREIGN KEY
	(
	PersonID,
	TenantID
	) REFERENCES dbo.Person
	(
	PersonID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_StormwaterAssessmentType_StormwaterAssessmentTypeID FOREIGN KEY
	(
	StormwaterAssessmentTypeID
	) REFERENCES dbo.StormwaterAssessmentType
	(
	StormwaterAssessmentTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID FOREIGN KEY
	(
	TreatmentBMPID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT
	FK_TreatmentBMPAssessment_Tenant_TenantID FOREIGN KEY
	(
	TenantID
	) REFERENCES dbo.Tenant
	(
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
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

alter table dbo.TreatmentBMPAssessment add constraint FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID foreign key (TreatmentBMPTypeID) references dbo.TreatmentBMPType(TreatmentBMPTypeID)  ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPObservation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


