MERGE INTO dbo.WaterQualityManagementPlanVisitStatus AS Target
USING (VALUES
(1, 'Initial Annual Verification', 'Initial Annual Verification'),
(2, 'Follow-up Verification', 'Follow-up Verification')
)
AS Source (WaterQualityManagementPlanVisitStatusID, WaterQualityManagementPlanVisitStatusName, WaterQualityManagementPlanVisitStatusDisplayName)
ON Target.WaterQualityManagementPlanVisitStatusID = Source.WaterQualityManagementPlanVisitStatusID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanVisitStatusName = Source.WaterQualityManagementPlanVisitStatusName,
	WaterQualityManagementPlanVisitStatusDisplayName = Source.WaterQualityManagementPlanVisitStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanVisitStatusID, WaterQualityManagementPlanVisitStatusName, WaterQualityManagementPlanVisitStatusDisplayName)
	VALUES (WaterQualityManagementPlanVisitStatusID, WaterQualityManagementPlanVisitStatusName, WaterQualityManagementPlanVisitStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
