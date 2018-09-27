CREATE TABLE WaterQualityManagementPlanVerifyType(
    WaterQualityManagementPlanVerifyTypeID INT NOT NULL CONSTRAINT PK_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID PRIMARY KEY,
    TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyType_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanVerifyTypeName VARCHAR(100) NOT NULL
);

INSERT INTO WaterQualityManagementPlanVerifyType (WaterQualityManagementPlanVerifyTypeID, TenantID, WaterQualityManagementPlanVerifyTypeName)
VALUES 
(1, 2, 'Jurisdiction Performed'),
(2, 2, 'Self Certification');


CREATE TABLE WaterQualityManagementPlanVisitStatus(
    WaterQualityManagementPlanVisitStatusID INT NOT NULL CONSTRAINT PK_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID PRIMARY KEY,
    TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVisitStatus_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanVisitStatusName VARCHAR(100) NOT NULL
);

INSERT INTO WaterQualityManagementPlanVisitStatus (WaterQualityManagementPlanVisitStatusID, TenantID, WaterQualityManagementPlanVisitStatusName)
VALUES 
(1, 2, 'Initial Annual Verify'),
(2, 2, 'Follow-up Verify');

CREATE TABLE WaterQualityManagementPlanVerifyStatus(
    WaterQualityManagementPlanVerifyStatusID INT NOT NULL CONSTRAINT PK_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID PRIMARY KEY,
    TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyStatus_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanVerifyStatusName VARCHAR(100) NOT NULL
);


INSERT INTO WaterQualityManagementPlanVerifyStatus (WaterQualityManagementPlanVerifyStatusID, TenantID, WaterQualityManagementPlanVerifyStatusName)
VALUES 
(1, 2, 'Verify Adequate O&M of WQMP'),
(2, 2, 'Deficiencies are Present and Follow-up is Required');



CREATE TABLE dbo.WaterQualityManagementPlanPhoto(
    WaterQualityManagementPlanPhotoID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID PRIMARY KEY,
    TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanPhoto_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    FileResourceID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanPhoto_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource (FileResourceID),
    Caption varchar(500) NULL,
	UploadDate DATETIME NOT NULL
);






CREATE TABLE WaterQualityManagementPlanVerify (
    WaterQualityManagementPlanVerifyID  INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID PRIMARY KEY,
    TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlan_WaterQualityManagementPlanID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlan (WaterQualityManagementPlanID),
    WaterQualityManagementPlanVerifyTypeID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerifyType (WaterQualityManagementPlanVerifyTypeID),
    WaterQualityManagementPlanVisitStatusID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVisitStatus (WaterQualityManagementPlanVisitStatusID), 
    WaterQualityManagementPlanDocumentID INT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanDocument (WaterQualityManagementPlanDocumentID),    
    WaterQualityManagementPlanVerifyStatusID INT NOT NULL CONSTRAINT  FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerifyStatus (WaterQualityManagementPlanVerifyStatusID),
	LastEditedByPersonID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerify_Person_LastEditedByPersonID_PersonID FOREIGN KEY REFERENCES dbo.Person (PersonID),
	SourceControlCondition VARCHAR(1000) NULL,
	EnforcementOrFollowupActions VARCHAR(1000) NULL,
    LastEditedDate DATETIME NOT NULL
);


CREATE TABLE WaterQualityManagementPlanVerifyPhoto(
	WaterQualityManagementPlanVerifyPhotoID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerifyPhotoID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyPhoto_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	WaterQualityManagementPlanVerifyID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerify (WaterQualityManagementPlanVerifyID),
	WaterQualityManagementPlanPhotoID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanPhoto (WaterQualityManagementPlanPhotoID),
);


CREATE TABLE WaterQualityManagementPlanVerifySourceControlBMP(
	WaterQualityManagementPlanVerifySourceControlBMPID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerifySourceControlBMPID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifySourceControlBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	WaterQualityManagementPlanVerifyID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerify (WaterQualityManagementPlanVerifyID),
	SourceControlBMPID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifySourceControlBMP_SourceControlBMP_SourceControlBMPID FOREIGN KEY REFERENCES dbo.SourceControlBMP (SourceControlBMPID),
	WaterQualityManagementPlanSourceControlCondition VARCHAR(1000)
);



CREATE TABLE WaterQualityManagementPlanVerifyQuickBMP (
	WaterQualityManagementPlanVerifyQuickBMPID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerifyQuickBMPID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyQuickBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	WaterQualityManagementPlanVerifyID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerify (WaterQualityManagementPlanVerifyID),
	QuickBMPID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMP_QuickBMPID FOREIGN KEY REFERENCES dbo.QuickBMP (QuickBMPID),
	IsAdequate BIT NULL,
	WaterQualityManagementPlanVerifyQuickBMPNote VARCHAR(500) NULL
);


CREATE TABLE WaterQualityManagementPlanVerifyTreatmentBMP (
	WaterQualityManagementPlanVerifyTreatmentBMPID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerifyTreatmentBMPID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyTreatmentBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	WaterQualityManagementPlanVerifyID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanVerify (WaterQualityManagementPlanVerifyID),
	TreatmentBMPID INT NOT NULL CONSTRAINT FK_WaterQualityManagementPlanVerifyQuickBMP_TreatmentBMP_TreatmentBMPID FOREIGN KEY REFERENCES dbo.TreatmentBMP (TreatmentBMPID),
	IsAdequate BIT NULL,
	WaterQualityManagementPlanVerifyQuickBMPNote VARCHAR(500) NULL
);
