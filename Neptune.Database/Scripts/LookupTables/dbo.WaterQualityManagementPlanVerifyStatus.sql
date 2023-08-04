MERGE INTO dbo.WaterQualityManagementPlanVerifyStatus AS Target
USING (VALUES
(1, 'Verify Adequate O&M of WQMP', 'Verify Adequate O&M of WQMP'),
(2, 'Deficiencies are Present and Follow-up is Required', 'Deficiencies are Present and Follow-up is Required')
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
