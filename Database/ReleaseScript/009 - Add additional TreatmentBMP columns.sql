CREATE TABLE dbo.TreatmentBMPDocument(
	TreatmentBMPDocumentID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPDocument_TreatmentBMPDocumentID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPDocument_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	FileResourceID int NOT NULL CONSTRAINT FK_TreatmentBMPDocument_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource (FileResourceID),
	TreatmentBMPID int NOT NULL CONSTRAINT FK_TreatmentBMPDocument_TreatmentBMP_TreatmentBMPID FOREIGN KEY REFERENCES dbo.TreatmentBMP (TreatmentBMPID),
	DisplayName varchar(200) NOT NULL,
	UploadDate date NOT NULL,
	DocumentDescription varchar(500) null,
	CONSTRAINT AK_TreatmentBMPDocument_FileResourceID_TreatmentBMPID UNIQUE (FileResourceID, TreatmentBMPID),
	CONSTRAINT FK_TreatmentBMPDocument_FileResource_FileResourceID_TenantID FOREIGN KEY(FileResourceID, TenantID) REFERENCES dbo.FileResource (FileResourceID, TenantID),
	CONSTRAINT FK_TreatmentBMPDocument_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY(TreatmentBMPID, TenantID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID, TenantID)
)

CREATE TABLE dbo.TreatmentBMPPhoto(
	TreatmentBMPPhotoID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPPhoto_TreatmentBMPPhotoID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPPhoto_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	FileResourceID int NOT NULL CONSTRAINT FK_TreatmentBMPPhoto_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource (FileResourceID),
	TreatmentBMPID int NOT NULL CONSTRAINT FK_TreatmentBMPPhoto_TreatmentBMP_TreatmentBMPID FOREIGN KEY REFERENCES dbo.TreatmentBMP (TreatmentBMPID),
	DisplayName varchar(200) NOT NULL,
	UploadDate date NOT NULL,
	PhotoDescription varchar(500) null,
	CONSTRAINT AK_TreatmentBMPPhoto_FileResourceID_TreatmentBMPID UNIQUE (FileResourceID, TreatmentBMPID),
	CONSTRAINT FK_TreatmentBMPPhoto_FileResource_FileResourceID_TenantID FOREIGN KEY(FileResourceID, TenantID) REFERENCES dbo.FileResource (FileResourceID, TenantID),
	CONSTRAINT FK_TreatmentBMPPhoto_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY(TreatmentBMPID, TenantID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID, TenantID)
)




alter table dbo.TreatmentBMP add SystemOfRecordID varchar(100) null, YearBuilt int null, OwnerOrganizationID int null
alter table dbo.TreatmentBMP add constraint FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID foreign key (OwnerOrganizationID) references dbo.Organization(OrganizationID)
GO

update dbo.TreatmentBMP
set SystemOfRecordID = concat('Sitka', TreatmentBMPID), OwnerOrganizationID = 1

alter table dbo.TreatmentBMP alter column SystemOfRecordID varchar(100) not null
alter table dbo.TreatmentBMP alter column OwnerOrganizationID int not null



