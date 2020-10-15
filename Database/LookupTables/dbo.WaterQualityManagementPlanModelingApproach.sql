delete from dbo.WaterQualityManagementPlanModelingApproach
insert into dbo.WaterQualityManagementPlanModelingApproach(WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
values
(1, 'Detailed', 'Detailed', 'This WQMP is modeled by explicitly inventorying all associated structural BMPs.'),
(2, 'Simplified', 'Simplified', 'This BMP is modeled by entering simplified structural BMP parameters directly on this WQMP')
