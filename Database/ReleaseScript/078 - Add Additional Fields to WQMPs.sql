
CREATE TABLE WaterQualityManagementPlanPermitTerm (
    WaterQualityManagementPlanPermitTermID INT NOT NULL CONSTRAINT PK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID PRIMARY KEY,
    WaterQualityManagementPlanPermitTermName VARCHAR(100) NOT NULL CONSTRAINT AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermName UNIQUE,
    WaterQualityManagementPlanPermitTermDisplayName VARCHAR(100) NOT NULL CONSTRAINT AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermDisplayName UNIQUE,
    SortOrder INT NOT NULL 
);
GO


ALTER TABLE dbo.WaterQualityManagementPlan
ADD WaterQualityManagementPlanPermitTermID INT NULL CONSTRAINT FK_WaterQualityManagementPlan_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID FOREIGN KEY REFERENCES dbo.WaterQualityManagementPlanPermitTerm (WaterQualityManagementPlanPermitTermID);




CREATE TABLE HydrologicSubarea (
    HydrologicSubareaID INT NOT NULL CONSTRAINT PK_HydrologicSubarea_HydrologicSubareaID PRIMARY KEY,
	HydrologicSubareaName VARCHAR(100) NOT NULL CONSTRAINT AK_HydrologicSubarea_HydrologicSubareaName UNIQUE, 
	HydrologicSubareaDisplayName VARCHAR(100) NOT NULL CONSTRAINT AK_HydrologicSubarea_HydrologicSubareaDisplayName UNIQUE, 
	SortOrder INT NOT NULL 
);
GO



ALTER TABLE dbo.WaterQualityManagementPlan
ADD HydrologicSubareaID INT NULL CONSTRAINT FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID FOREIGN KEY REFERENCES dbo.HydrologicSubarea (HydrologicSubareaID);




CREATE TABLE HydromodificationApplies (
    HydromodificationAppliesID INT NOT NULL CONSTRAINT PK_HydromodificationApplies_HydromodificationAppliesID PRIMARY KEY,
	HydromodificationAppliesName VARCHAR(100) NOT NULL CONSTRAINT AK_HydromodificationApplies_HydromodificationAppliesName UNIQUE, 
	HydromodificationAppliesDisplayName VARCHAR(100) NOT NULL CONSTRAINT AK_HydromodificationApplies_HydromodificationAppliesDisplayName UNIQUE, 
	SortOrder INT NOT NULL 
);
GO


ALTER TABLE dbo.WaterQualityManagementPlan
ADD HydromodificationAppliesID  INT NULL CONSTRAINT FK_WaterQualityManagementPlan_HydromodificationApplies_HydromodificationAppliesID FOREIGN KEY REFERENCES dbo.HydromodificationApplies (HydromodificationAppliesID);



ALTER TABLE dbo.WaterQualityManagementPlan
ADD DateOfContruction  DATETIME NULL;