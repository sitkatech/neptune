CREATE TABLE [dbo].[WaterQualityManagementPlanLandUse](
	[WaterQualityManagementPlanLandUseID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseID] PRIMARY KEY,
	[WaterQualityManagementPlanLandUseName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseName] UNIQUE,
	[WaterQualityManagementPlanLandUseDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanLandUse_WaterQualityManagementPlanLandUseDisplayName] UNIQUE,
	SortOrder int not null
)
