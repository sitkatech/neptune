alter table dbo.WaterQualityManagementPlanModelingApproach alter column WaterQualityManagementPlanModelingApproachDescription varchar(300) not null

update dbo.WaterQualityManagementPlanModelingApproach
set WaterQualityManagementPlanModelingApproachDescription = 'This WQMP is modeled by inventorying the associated structural BMPs and defining their delineations. The performance of each BMP is modeled based on its modeling parameters and the attributes of the delineated tributary area.'
where WaterQualityManagementPlanModelingApproachName = 'Detailed'

update dbo.WaterQualityManagementPlanModelingApproach
set WaterQualityManagementPlanModelingApproachDescription = 'This WQMP is modeled by entering simplified structural BMP modeling parameters directly on this WQMP page.'
where WaterQualityManagementPlanModelingApproachName = 'Simplified'