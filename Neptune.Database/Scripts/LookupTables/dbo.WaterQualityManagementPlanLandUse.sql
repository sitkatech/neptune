MERGE INTO dbo.WaterQualityManagementPlanLandUse AS Target
USING (VALUES
(1, 'Residential', 'Residential', 70),
(2, 'Commercial', 'Commercial', 10),
(3, 'Industrial', 'Industrial', 30),
(4, 'Other', 'Other', 90),
(5, 'Road', 'Road', 80),
(6, 'Flood', 'Flood', 20),
(7, 'Municipal', 'Municipal', 50),
(8, 'Park', 'Park', 60),
(9, 'Mixed', 'Mixed', 40)
)
AS Source (WaterQualityManagementPlanLandUseID, WaterQualityManagementPlanLandUseName, WaterQualityManagementPlanLandUseDisplayName, SortOrder)
ON Target.WaterQualityManagementPlanLandUseID = Source.WaterQualityManagementPlanLandUseID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanLandUseName = Source.WaterQualityManagementPlanLandUseName,
	WaterQualityManagementPlanLandUseDisplayName = Source.WaterQualityManagementPlanLandUseDisplayName,
	SortOrder = Source.SortOrder
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanLandUseID, WaterQualityManagementPlanLandUseName, WaterQualityManagementPlanLandUseDisplayName, SortOrder)
	VALUES (WaterQualityManagementPlanLandUseID, WaterQualityManagementPlanLandUseName, WaterQualityManagementPlanLandUseDisplayName, SortOrder)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;

