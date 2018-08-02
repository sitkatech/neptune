create table dbo.WaterQualityManagementPlanDocumentType(
WaterQualityManagementPlanDocumentTypeID int not null constraint PK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID Primary Key,
WaterQualityManagementPlanDocumentTypeName varchar(100) not null constraint AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeName unique,
WaterQualityManagementPlanDocumentTypeDisplayName varchar(100) not null constraint AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeDisplayName unique,
IsRequired bit not null
)
go

insert into dbo.WaterQualityManagementPlanDocumentType (WaterQualityManagementPlanDocumentTypeID, WaterQualityManagementPlanDocumentTypeName, WaterQualityManagementPlanDocumentTypeDisplayName, IsRequired)
values
(1, 'FinalWQMP', 'Final WQMP', 1),
(2, 'AsBuiltDrawings', 'As-built drawings', 1),
(3, 'OMPlan', 'O&M Plan', 1),
(4, 'Other', 'Other', 0)

alter table dbo.WaterQualityManagementPlanDocument
add WaterQualityManagementPlanDocumentTypeID int null
go

alter table dbo.WaterQualityManagementPlanDocument
add constraint FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID
	foreign key (WaterQualityManagementPlanDocumentTypeID)
	references dbo.WaterQualityManagementPlanDocumentType(WaterQualityManagementPlanDocumentTypeID)

update dbo.WaterQualityManagementPlanDocument
set WaterQualityManagementPlanDocumentTypeID = 4

alter table dbo.WaterQualityManagementPlanDocument
alter column WaterQualityManagementPlanDocumentTypeID int not null