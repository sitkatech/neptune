drop table dbo.FieldDefinitionDataImage
alter table dbo.FieldDefinition drop column CanCustomizeLabel


exec sp_rename 'dbo.FK_FieldDefinitionData_FieldDefinition_FieldDefinitionID', 'FK_FieldDefinitionData_FieldDefinitionType_FieldDefinitionTypeID', 'OBJECT';
exec sp_rename 'dbo.PK_FieldDefinition_FieldDefinitionID', 'PK_FieldDefinitionType_FieldDefinitionTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_FieldDefinition_FieldDefinitionDisplayName', 'AK_FieldDefinitionType_FieldDefinitionTypeDisplayName', 'OBJECT';
exec sp_rename 'dbo.AK_FieldDefinition_FieldDefinitionName', 'AK_FieldDefinitionType_FieldDefinitionTypeName', 'OBJECT';
exec sp_rename 'dbo.FieldDefinition.FieldDefinitionID', 'FieldDefinitionTypeID', 'COLUMN';
exec sp_rename 'dbo.FieldDefinition.FieldDefinitionName', 'FieldDefinitionTypeName', 'COLUMN';
exec sp_rename 'dbo.FieldDefinition.FieldDefinitionDisplayName', 'FieldDefinitionTypeDisplayName', 'COLUMN';
exec sp_rename 'dbo.FieldDefinitionData.FieldDefinitionID', 'FieldDefinitionTypeID', 'COLUMN';
exec sp_rename 'dbo.FieldDefinition', 'FieldDefinitionType';


exec sp_rename 'dbo.FK_FieldDefinitionData_FieldDefinitionType_FieldDefinitionTypeID', 'FK_FieldDefinition_FieldDefinitionType_FieldDefinitionTypeID', 'OBJECT';
exec sp_rename 'dbo.PK_FieldDefinitionData_FieldDefinitionDataID', 'PK_FieldDefinition_FieldDefinitionID', 'OBJECT';
exec sp_rename 'dbo.FieldDefinitionData.FieldDefinitionDataID', 'FieldDefinitionID', 'COLUMN';
exec sp_rename 'dbo.FieldDefinitionData.FieldDefinitionDataValue', 'FieldDefinitionValue', 'COLUMN';
exec sp_rename 'dbo.FieldDefinitionData', 'FieldDefinition';


insert into dbo.FieldDefinition(FieldDefinitionTypeID, FieldDefinitionValue)
select fdt.FieldDefinitionTypeID, fdt.DefaultDefinition as FieldDefinitionValue
from dbo.FieldDefinitionType fdt
left join dbo.FieldDefinition fd on fdt.FieldDefinitionTypeID = fd.FieldDefinitionTypeID
where fd.FieldDefinitionID is null

update fd
set fd.FieldDefinitionValue = fdt.DefaultDefinition
from dbo.FieldDefinitionType fdt
join dbo.FieldDefinition fd on fdt.FieldDefinitionTypeID = fd.FieldDefinitionTypeID
where fd.FieldDefinitionValue is null



alter table dbo.FieldDefinition drop column FieldDefinitionLabel
alter table dbo.FieldDefinitionType drop column DefaultDefinition