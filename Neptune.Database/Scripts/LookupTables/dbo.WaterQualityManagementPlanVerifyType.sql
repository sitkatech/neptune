MERGE INTO dbo.WaterQualityManagementPlanVerifyType AS Target
USING (VALUES
(1, 'Jurisdiction Performed', 'Jurisdiction Performed'),
(2, 'Self Certification', 'Self Certification')
)
AS Source (WaterQualityManagementPlanTypeID, WaterQualityManagementPlanTypeName, WaterQualityManagementPlanTypeDisplayName)
ON Target.WaterQualityManagementPlanTypeID = Source.WaterQualityManagementPlanTypeID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanTypeName = Source.WaterQualityManagementPlanTypeName,
	WaterQualityManagementPlanTypeDisplayName = Source.WaterQualityManagementPlanTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanTypeID, WaterQualityManagementPlanTypeName, WaterQualityManagementPlanTypeDisplayName)
	VALUES (WaterQualityManagementPlanTypeID, WaterQualityManagementPlanTypeName, WaterQualityManagementPlanTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
