CREATE TABLE QuickBMP (
    QuickBMPID  INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_QuickBMP_QuickBMPID PRIMARY KEY, 
	TenantID INT NOT NULL CONSTRAINT FK_QuickBMP_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
    WaterQualityManagementPlanID INT NOT NULL CONSTRAINT FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlan (WaterQualityManagementPlanID),
    TreatmentBMPTypeID INT NOT NULL CONSTRAINT FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPType (TreatmentBMPTypeID),
    QuickBMPName VARCHAR(100) NOT NULL,
    QuickBMPNote VARCHAR(100) NULL,
    Constraint AK_QuickBMP_QuickBMPName_QuickBMPID unique(QuickBMPName, QuickBMPID)
);