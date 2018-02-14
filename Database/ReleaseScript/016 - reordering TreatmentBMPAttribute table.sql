/*
   Wednesday, February 14, 20182:00:06 AM
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
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID
GO
ALTER TABLE dbo.TreatmentBMPTypeAttributeType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID
GO
ALTER TABLE dbo.TreatmentBMPAttributeType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID
GO
ALTER TABLE dbo.TreatmentBMPType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID
GO
ALTER TABLE dbo.TreatmentBMP SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TreatmentBMPAttribute
	DROP CONSTRAINT FK_TreatmentBMPAttribute_Tenant_TenantID
GO
ALTER TABLE dbo.Tenant SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_TreatmentBMPAttribute
	(
	TreatmentBMPAttributeID int NOT NULL IDENTITY (1, 1),
	TenantID int NOT NULL,
	TreatmentBMPID int NOT NULL,
	TreatmentBMPTypeAttributeTypeID int NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	TreatmentBMPAttributeTypeID int NOT NULL,
	TreatmentBMPAttributeValue varchar(1000) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TreatmentBMPAttribute SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPAttribute ON
GO
IF EXISTS(SELECT * FROM dbo.TreatmentBMPAttribute)
	 EXEC('INSERT INTO dbo.Tmp_TreatmentBMPAttribute (TreatmentBMPAttributeID, TenantID, TreatmentBMPID, TreatmentBMPTypeAttributeTypeID, TreatmentBMPTypeID, TreatmentBMPAttributeTypeID, TreatmentBMPAttributeValue)
		SELECT TreatmentBMPAttributeID, TenantID, TreatmentBMPID, TreatmentBMPTypeAttributeTypeID, TreatmentBMPTypeID, TreatmentBMPAttributeTypeID, TreatmentBMPAttributeValue FROM dbo.TreatmentBMPAttribute WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TreatmentBMPAttribute OFF
GO
DROP TABLE dbo.TreatmentBMPAttribute
GO
EXECUTE sp_rename N'dbo.Tmp_TreatmentBMPAttribute', N'TreatmentBMPAttribute', 'OBJECT' 
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	PK_TreatmentBMPAttribute_TreatmentBMPAttributeID PRIMARY KEY CLUSTERED 
	(
	TreatmentBMPAttributeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	AK_TreatmentBMPAttribute_TreatmentBMPTypeID_TreatmentBMPTypeAttributeTypeID UNIQUE NONCLUSTERED 
	(
	TreatmentBMPID,
	TreatmentBMPTypeID,
	TreatmentBMPAttributeTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_Tenant_TenantID FOREIGN KEY
	(
	TenantID
	) REFERENCES dbo.Tenant
	(
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID FOREIGN KEY
	(
	TreatmentBMPID
	) REFERENCES dbo.TreatmentBMP
	(
	TreatmentBMPID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY
	(
	TreatmentBMPTypeID
	) REFERENCES dbo.TreatmentBMPType
	(
	TreatmentBMPTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID FOREIGN KEY
	(
	TreatmentBMPAttributeTypeID
	) REFERENCES dbo.TreatmentBMPAttributeType
	(
	TreatmentBMPAttributeTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID FOREIGN KEY
	(
	TreatmentBMPTypeID,
	TreatmentBMPAttributeTypeID
	) REFERENCES dbo.TreatmentBMPTypeAttributeType
	(
	TreatmentBMPTypeID,
	TreatmentBMPAttributeTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY
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
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID FOREIGN KEY
	(
	TreatmentBMPTypeID,
	TenantID
	) REFERENCES dbo.TreatmentBMPType
	(
	TreatmentBMPTypeID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID FOREIGN KEY
	(
	TreatmentBMPAttributeTypeID,
	TenantID
	) REFERENCES dbo.TreatmentBMPAttributeType
	(
	TreatmentBMPAttributeTypeID,
	TenantID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TreatmentBMPAttribute ADD CONSTRAINT
	FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID FOREIGN KEY
	(
	TreatmentBMPTypeAttributeTypeID
	) REFERENCES dbo.TreatmentBMPTypeAttributeType
	(
	TreatmentBMPTypeAttributeTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
