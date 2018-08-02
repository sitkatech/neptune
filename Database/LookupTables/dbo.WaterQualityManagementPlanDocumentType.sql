delete from dbo.WaterQualityManagementPlanDocumentType

insert into dbo.WaterQualityManagementPlanDocumentType (WaterQualityManagementPlanDocumentTypeID, WaterQualityManagementPlanDocumentTypeName, WaterQualityManagementPlanDocumentTypeDisplayName, IsRequired)
values
(1, 'FinalWQMP', 'Final WQMP', 1),
(2, 'AsBuiltDrawings', 'As-built drawings', 1),
(3, 'OMPlan', 'O&M Plan', 1),
(4, 'Other', 'Other', 0)