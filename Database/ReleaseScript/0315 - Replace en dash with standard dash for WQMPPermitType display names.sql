update dbo.WaterQualityManagementPlanPermitTerm
set WaterQualityManagementPlanPermitTermDisplayName = replace(WaterQualityManagementPlanPermitTermDisplayName, '–', '-')