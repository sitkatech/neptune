MERGE INTO dbo.WaterQualityManagementPlanVerifyStatus AS Target
USING (VALUES
(1, 'Adequate O&M of WQMP is Verified', 'Adequate O&M of WQMP is Verified'),
(2, 'Deficiencies are Present and Follow-up is Required', 'Deficiencies are Present and Follow-up is Required')
)
AS Source (WaterQualityManagementPlanVerifyStatusID, WaterQualityManagementPlanVerifyStatusName, WaterQualityManagementPlanVerifyStatusDisplayName)
ON Target.WaterQualityManagementPlanVerifyStatusID = Source.WaterQualityManagementPlanVerifyStatusID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanVerifyStatusName = Source.WaterQualityManagementPlanVerifyStatusName,
	WaterQualityManagementPlanVerifyStatusDisplayName = Source.WaterQualityManagementPlanVerifyStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanVerifyStatusID, WaterQualityManagementPlanVerifyStatusName, WaterQualityManagementPlanVerifyStatusDisplayName)
	VALUES (WaterQualityManagementPlanVerifyStatusID, WaterQualityManagementPlanVerifyStatusName, WaterQualityManagementPlanVerifyStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
