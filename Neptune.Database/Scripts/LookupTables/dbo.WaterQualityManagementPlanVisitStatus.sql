MERGE INTO dbo.WaterQualityManagementPlanStatus AS Target
USING (VALUES
(1, 'Initial Annual Verification', 'Initial Annual Verification'),
(2, 'Follow-up Verification', 'Follow-up Verification')
)
AS Source (WaterQualityManagementPlanStatusID, WaterQualityManagementPlanStatusName, WaterQualityManagementPlanStatusDisplayName)
ON Target.WaterQualityManagementPlanStatusID = Source.WaterQualityManagementPlanStatusID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanStatusName = Source.WaterQualityManagementPlanStatusName,
	WaterQualityManagementPlanStatusDisplayName = Source.WaterQualityManagementPlanStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanStatusID, WaterQualityManagementPlanStatusName, WaterQualityManagementPlanStatusDisplayName)
	VALUES (WaterQualityManagementPlanStatusID, WaterQualityManagementPlanStatusName, WaterQualityManagementPlanStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
