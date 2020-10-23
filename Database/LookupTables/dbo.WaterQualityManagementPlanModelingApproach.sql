delete from dbo.WaterQualityManagementPlanModelingApproach
insert into dbo.WaterQualityManagementPlanModelingApproach(WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
values
(1, 'Detailed', 'Detailed', 'This WQMP is modeled by inventorying the associated structural BMPs and defining their delineations. The performance of each BMP is modeled based on its modeling parameters and the attributes of the delineated tributary area.'),
(2, 'Simplified', 'Simplified', 'This WQMP is modeled by entering simplified structural BMP modeling parameters directly on this WQMP page.')
