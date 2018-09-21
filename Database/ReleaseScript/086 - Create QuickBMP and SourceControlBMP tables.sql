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
	SourceControlBMPAttributeCategoryID INT NOT NULL CONSTRAINT PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_SourceControlBMPAttributeCategory_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	SourceControlBMPAttributeCategoryName VARCHAR(100) NOT NULL
);


CREATE Table dbo.SourceControlBMPAttribute (
	SourceControlBMPAttributeID INT NOT NULL CONSTRAINT PK_SourceControlBMPAttribute_SourceControlBMPAttributeID PRIMARY KEY,
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








INSERT INTO dbo.SourceControlBMPAttributeCategory (SourceControlBMPAttributeCategoryID, TenantID, SourceControlBMPAttributeCategoryName)
VALUES 
(1, 2, 'Site Design BMPs'),
(2, 2, 'Applicable Routine Non-Structural Source Control BMPs'),
(3, 2, 'Applicable Routine Structural Source Control BMPs');



INSERT INTO dbo.SourceControlBMPAttribute(SourceControlBMPAttributeID, TenantID, SourceControlBMPAttributeCategoryID, SourceControlBMPAttributeName)
VALUES
(1, 2, 1, 'Localized On-lot Infiltration'),
(2, 2, 1, 'Impervious Area Dispersion'),
(3, 2, 1, 'Street Trees'),
(4, 2, 1, 'Harvest and Use Systems'),
(5, 2, 1, 'Green Roof/Brown Roof'),
(6, 2, 1, 'Distributed Permeable Pavement in Low Traffic Areas'),
(7, 2, 1, 'Absorbent Landscaping with Drought Tolerant Species'),
(8, 2, 1, 'Buffer Zones for Natural Water Bodies'),
(9, 2, 1, 'Maintained or Restored Natural Drainage Patterns '),
(10, 2, 1, 'Conserved Natural Areas'),
(11, 2, 1, 'Minimized Impervious Areas'),

(12, 2, 2, 'Education for Property Owners, Tenants and Occupants'),
(13, 2, 2, 'Activity Restrictions'),
(14, 2, 2, 'Landscape Management'),
(15, 2, 2, 'BMP Maintenance'),
(16, 2, 2, 'Title 22 CCR Compliance'),
(17, 2, 2, 'Local Water Quality Permit Compliance'),
(18, 2, 2, 'Spill Contingency Plan'),
(19, 2, 2, 'Underground Storage Tank Compliance'),
(20, 2, 2, 'Hazardous Materials Disclosure Compliance'),
(21, 2, 2, 'Uniform Fire Code Implementation'),
(22, 2, 2, 'Litter Control'),
(23, 2, 2, 'Employee Training'),
(24, 2, 2, 'Housekeeping of Loading Docks'),
(25, 2, 2, 'Catch Basin Inspection'),
(26, 2, 2, 'Street Sweeping Private Streets and Parking Lots'),
(27, 2, 2, 'Retail Gasoline Outlets'),

(28, 2, 3, 'Provide Storm Drain System Stenciling and Signage'),
(29, 2, 3, 'Design Outdoor Hazardous Material Storage Areas to Reduce Pollutant Introduction'),
(30, 2, 3, 'Design Trash Enclosures to Reduce Pollutant Introduction '),
(31, 2, 3, 'Use Efficient Irrigation Systems and Landscape Design'),
(32, 2, 3, 'Dry Weather Flow Source Prohibition for Areas Not Draining to LID BMPs'),
(33, 2, 3, 'Protect Slopes and Channels'),
(34, 2, 3, 'Loading Dock Areas'),
(35, 2, 3, 'Maintenance Bays'),
(36, 2, 3, 'Vehicle Wash Areas'),
(37, 2, 3, 'Outdoor Processing Areas'),
(38, 2, 3, 'Equipment Wash Areas'),
(39, 2, 3, 'Fueling Areas'),
(40, 2, 3, 'Site Design and Landscape Planning (Hillside Landscaping)'),
(41, 2, 3, 'Wash Water Controls for Food Preparation Areas'),
(42, 2, 3, 'Community Car Wash Racks');