MERGE INTO dbo.WaterQualityManagementPlanVerifyType AS Target
USING (VALUES
(1, 'Jurisdiction Performed', 'Jurisdiction Performed'),
(2, 'Self Certification', 'Self Certification')
)
AS Source (WaterQualityManagementPlanVerifyTypeID, WaterQualityManagementPlanVerifyTypeName, WaterQualityManagementPlanVerifyTypeDisplayName)
ON Target.WaterQualityManagementPlanVerifyTypeID = Source.WaterQualityManagementPlanVerifyTypeID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanVerifyTypeName = Source.WaterQualityManagementPlanVerifyTypeName,
	WaterQualityManagementPlanVerifyTypeDisplayName = Source.WaterQualityManagementPlanVerifyTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanVerifyTypeID, WaterQualityManagementPlanVerifyTypeName, WaterQualityManagementPlanVerifyTypeDisplayName)
	VALUES (WaterQualityManagementPlanVerifyTypeID, WaterQualityManagementPlanVerifyTypeName, WaterQualityManagementPlanVerifyTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
