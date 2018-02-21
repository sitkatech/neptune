create table dbo.MaintenanceRecordType(
MaintenanceRecordTypeID int not null constraint PK_MaintenanceRecordType_MaintenanceRecordTypeID primary key,
MaintenanceRecordTypeName varchar(30) not null constraint AK_MaintenanceRecordType_MaintenanceRecordTypeName unique,
MaintenanceRecordTypeDisplayName varchar(30) not null constraint AK_MaintenanceRecordType_MaintenanceRecordTypeDisplayName unique
)
go

create table dbo.MaintenanceRecord(
MaintenanceRecordID int not null identity(1,1) constraint PK_MaintenanceRecord_MaintenanceRecordID primary key,
TenantID int not null constraint FK_MaintenanceRecord_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
TreatmentBMPID int not null constraint FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
MaintenanceRecordDate date not null,
PerformedByPersonID int not null constraint FK_MaintenanceRecord_Person_PerformedByPersonID_PersonID foreign key references dbo.Person(PersonID),
MaintenanceRecordDescription varchar(500) null,
MaintenanceRecordTypeID int not null constraint FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID foreign key references dbo.MaintenanceRecordType(MaintenanceRecordTypeID),
constraint AK_MaintenanceRecord_MaintenanceRecordID_TenantID unique nonclustered (MaintenanceRecordID, TenantID),
constraint FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TenantID foreign key (TreatmentBMPID, TenantID) references dbo.TreatmentBMP(TreatmentBMPID, TenantID)
)
go

insert into dbo.FieldDefinition(FieldDefinitionID, FieldDefinitionName, FieldDefinitionDisplayName, DefaultDefinition, CanCustomizeLabel)
values (41, N'MaintenanceRecordType', N'Maintenance Record Type', 'Whether the maintenance performed was Preventative or Corrective maintenance', 1),
(42, N'MaintenanceRecord', N'Maintenance Record', 'A record of a maintenance activity performed on a Treatment BMP', 1)