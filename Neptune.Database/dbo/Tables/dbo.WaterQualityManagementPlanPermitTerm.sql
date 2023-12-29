CREATE TABLE [dbo].[WaterQualityManagementPlanPermitTerm](
	[WaterQualityManagementPlanPermitTermID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID] PRIMARY KEY,
	[WaterQualityManagementPlanPermitTermName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermName] UNIQUE,
	[WaterQualityManagementPlanPermitTermDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermDisplayName] UNIQUE
)
