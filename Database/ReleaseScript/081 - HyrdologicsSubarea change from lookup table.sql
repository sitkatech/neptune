ALTER TABLE WaterQualityManagementPlan DROP CONSTRAINT FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID
GO

DROP TABLE HydrologicSubarea;
GO


CREATE TABLE HydrologicSubarea (
    HydrologicSubareaID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_HydrologicSubarea_HydrologicSubareaID PRIMARY KEY,
	TenantID INT NOT NULL CONSTRAINT FK_HydrologicSubarea_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	HydrologicSubareaName VARCHAR(100) NOT NULL CONSTRAINT AK_HydrologicSubarea_HydrologicSubareaName UNIQUE
);
GO

INSERT INTO dbo.HydrologicSubarea(TenantID, HydrologicSubareaName)
VALUES
(2, 'Aliso Creek'),
(2, 'Anaheim Bay-Huntington Harbor'),
(2, 'Coyote Creek-San Gabriel River'),
(2, 'Dana Point Coastal Streams'),
(2, 'Laguna Coastal Streams'),
(2, 'Newport Bay'),
(2, 'Newport Coastal Streams'),
(2, 'San Clemente Coastal Streams'),
(2, 'San Juan Creek'),
(2, 'San Mateo Creek'),
(2, 'Santa Ana River');



ALTER TABLE WaterQualityManagementPlan
DROP COLUMN HydrologicSubareaID

ALTER TABLE WaterQualityManagementPlan
ADD HydrologicSubareaID INT NULL CONSTRAINT FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID FOREIGN KEY REFERENCES dbo.HydrologicSubarea (HydrologicSubareaID);
