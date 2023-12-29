MERGE INTO dbo.WaterQualityManagementPlanDocumentType AS Target
USING (VALUES
(1, 'FinalWQMP', 'Final WQMP', 1),
(2, 'AsBuiltDrawings', 'As-built drawings', 1),
(3, 'OMPlan', 'O&M Plan', 1),
(4, 'Other', 'Other', 0)
)
AS Source (WaterQualityManagementPlanDocumentTypeID, WaterQualityManagementPlanDocumentTypeName, WaterQualityManagementPlanDocumentTypeDisplayName, IsRequired)
ON Target.WaterQualityManagementPlanDocumentTypeID = Source.WaterQualityManagementPlanDocumentTypeID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanDocumentTypeName = Source.WaterQualityManagementPlanDocumentTypeName,
	WaterQualityManagementPlanDocumentTypeDisplayName = Source.WaterQualityManagementPlanDocumentTypeDisplayName,
	IsRequired = Source.IsRequired
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanDocumentTypeID, WaterQualityManagementPlanDocumentTypeName, WaterQualityManagementPlanDocumentTypeDisplayName, IsRequired)
	VALUES (WaterQualityManagementPlanDocumentTypeID, WaterQualityManagementPlanDocumentTypeName, WaterQualityManagementPlanDocumentTypeDisplayName, IsRequired)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;