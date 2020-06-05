Create table dbo.DirtyModelNode(
DirtyModelNodeID int not null identity(1,1) constraint PK_DirtyModelNode_DirtyModelNodeID primary key,
TreatmentBMPID int null,
WaterQualityManagementPlanID int null,
RegionalSubbasinID int null,
DelineationID int null
)