ALTER TABLE WaterQualityManagementPlan DROP CONSTRAINT FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID
GO

DROP TABLE HydrologicSubarea;
GO


CREATE TABLE HydrologicSubarea (
    HydrologicSubareaID INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_HydrologicSubarea_HydrologicSubareaID PRIMARY KEY,
	HydrologicSubareaName VARCHAR(100) NOT NULL CONSTRAINT AK_HydrologicSubarea_HydrologicSubareaName UNIQUE
);
GO

INSERT INTO dbo.HydrologicSubarea(HydrologicSubareaName)
VALUES
('Aliso Creek'),
('Anaheim Bay-Huntington Harbor'),
('Coyote Creek-San Gabriel River'),
('Dana Point Coastal Streams'),
('Laguna Coastal Streams'),
('Newport Bay'),
('Newport Coastal Streams'),
('San Clemente Coastal Streams'),
('San Juan Creek'),
('San Mateo Creek'),
('Santa Ana River');



ALTER TABLE WaterQualityManagementPlan
DROP COLUMN HydrologicSubareaID

ALTER TABLE WaterQualityManagementPlan
ADD HydrologicSubareaID INT NULL CONSTRAINT FK_WaterQualityManagementPlan_HydrologicSubarea_HydrologicSubareaID FOREIGN KEY REFERENCES dbo.HydrologicSubarea (HydrologicSubareaID);
