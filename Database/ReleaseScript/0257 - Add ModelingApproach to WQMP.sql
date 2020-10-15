create table dbo.WaterQualityManagementPlanModelingApproach
(
	WaterQualityManagementPlanModelingApproachID int not null constraint PK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID primary key,
	WaterQualityManagementPlanModelingApproachName varchar(50) not null constraint AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachName unique,
	WaterQualityManagementPlanModelingApproachDisplayName varchar(50) not null constraint AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachDisplayName unique,
	WaterQualityManagementPlanModelingApproachDescription varchar(100) not null
)

alter table dbo.WaterQualityManagementPlan add WaterQualityManagementPlanModelingApproachID int null constraint FK_WaterQualityManagementPlan_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID foreign key references dbo.WaterQualityManagementPlanModelingApproach(WaterQualityManagementPlanModelingApproachID)
GO

insert into dbo.WaterQualityManagementPlanModelingApproach(WaterQualityManagementPlanModelingApproachID, WaterQualityManagementPlanModelingApproachName, WaterQualityManagementPlanModelingApproachDisplayName, WaterQualityManagementPlanModelingApproachDescription)
values
(1, 'Detailed', 'Detailed', 'This WQMP is modeled by explicitly inventorying all associated structural BMPs.'),
(2, 'Simplified', 'Simplified', 'This BMP is modeled by entering simplified structural BMP parameters directly on this WQMP')

-- defaulting them all to Detailed
update dbo.WaterQualityManagementPlan
set WaterQualityManagementPlanModelingApproachID = 1

alter table dbo.WaterQualityManagementPlan alter column WaterQualityManagementPlanModelingApproachID int not null
GO
