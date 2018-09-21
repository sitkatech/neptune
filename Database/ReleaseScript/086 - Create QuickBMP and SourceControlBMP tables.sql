CREATE TABLE dbo.QuickBMP (
    QuickBMPID  INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_QuickBMP_QuickBMPID PRIMARY KEY, 
	TenantID INT NOT NULL CONSTRAINT FK_QuickBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanID INT NOT NULL CONSTRAINT FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlan (WaterQualityManagementPlanID),
    TreatmentBMPTypeID INT NOT NULL CONSTRAINT FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPType (TreatmentBMPTypeID),
    QuickBMPName VARCHAR(100) NOT NULL,
    QuickBMPNote VARCHAR(200) NULL,
    Constraint AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName unique(WaterQualityManagementPlanID, QuickBMPName)
);


CREATE Table dbo.SourceControlBMPAttributeCategory (
	SourceControlBMPAttributeCategoryID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_SourceControlBMPAttributeCategory_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	SourceControlBMPAttributeCategoryName VARCHAR(100) NOT NULL
);


CREATE Table dbo.SourceControlBMPAttribute (
	SourceControlBMPAttributeID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_SourceControlBMPAttribute_SourceControlBMPAttributeID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_SourceControlBMPAttribute_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID), 
	SourceControlBMPAttributeCategoryID INT NOT NULL CONSTRAINT FK_SourceControlBMPAttribute_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID FOREIGN KEY REFERENCES dbo.SourceControlBMPAttributeCategory (SourceControlBMPAttributeCategoryID), 
	SourceControlBMPAttributeName VARCHAR(100) NOT NULL
);


CREATE TABLE dbo.SourceControlBMP(
	SourceControlBMPID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_SourceControlBMP_SourceControlBMPID PRIMARY KEY, 
	TenantID INT NOT NULL CONSTRAINT FK_SourceControlBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	WaterQualityManagementPlanID INT NOT NULL CONSTRAINT FK_SourceControlBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlan (WaterQualityManagementPlanID),
	SourceControlBMPAttributeID INT NOT NULL CONSTRAINT FK_SourceControlBMP_SourceControlBMPAttribute_SourceControlBMPAttributeID FOREIGN KEY REFERENCES dbo.SourceControlBMPAttribute (SourceControlBMPAttributeID), 
	IsPresent BIT NOT NULL,
    SourceControlBMPNote VARCHAR(200) NULL
);





