MERGE INTO dbo.WaterQualityManagementPlanModelingApproach AS Target
USING (VALUES
(1, 'Detailed', 'Detailed', 'This WQMP is modeled by inventorying the associated structural BMPs and defining their delineations. The performance of each BMP is modeled based on its modeling parameters and the attributes of the delineated tributary area.'),
(2, 'Simplified', 'Simplified', 'This WQMP is modeled by entering simplified structural BMP modeling parameters directly on this WQMP page.')
)
AS Source (WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
ON Target.WaterQualityManagementPlanModelingApproachID = Source.WaterQualityManagementPlanModelingApproachID
WHEN MATCHED THEN
UPDATE SET
	WaterQualityManagementPlanModelingApproachName = Source.WaterQualityManagementPlanModelingApproachName,
	WaterQualityManagementPlanModelingApproachDisplayName = Source.WaterQualityManagementPlanModelingApproachDisplayName,
	WaterQualityManagementPlanModelingApproachDescription = Source.WaterQualityManagementPlanModelingApproachDescription
WHEN NOT MATCHED BY TARGET THEN
	INSERT (WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
	VALUES (WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;