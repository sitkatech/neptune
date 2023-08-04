CREATE TABLE [dbo].[WaterQualityManagementPlanModelingApproach](
	[WaterQualityManagementPlanModelingApproachID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachID] PRIMARY KEY,
	[WaterQualityManagementPlanModelingApproachName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachName] UNIQUE,
	[WaterQualityManagementPlanModelingApproachDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanModelingApproach_WaterQualityManagementPlanModelingApproachDisplayName] UNIQUE,
	[WaterQualityManagementPlanModelingApproachDescription] [varchar](300) not null
)
