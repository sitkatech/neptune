alter table WaterQualityManagementPlan
add WaterQualityManagementPlanAreaInAcres [float] NULL


insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(67, 'EditWQMPBoundary', 'Refine WQMP Boundary Area')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(67, 'Use the editor tools on the map to refine the boundary area of this Water Quality Management Plan.')

