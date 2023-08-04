MERGE INTO dbo.WaterQualityManagementPlanPriority AS Target
USING (VALUES
(1, 'High', 'High'),
(2, 'Low', 'Low')
)
AS Source (WaterQualityManagementPlanPriorityID, WaterQualityManagementPlanPriorityName, WaterQualityManagementPlanPriorityDisplayName)
ON Target.WaterQualityManagementPlanPriorityID = Source.WaterQualityManagementPlanPriorityID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanPriorityName = Source.WaterQualityManagementPlanPriorityName,
	WaterQualityManagementPlanPriorityDisplayName = Source.WaterQualityManagementPlanPriorityDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanPriorityID, WaterQualityManagementPlanPriorityName, WaterQualityManagementPlanPriorityDisplayName)
	VALUES (WaterQualityManagementPlanPriorityID, WaterQualityManagementPlanPriorityName, WaterQualityManagementPlanPriorityDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
