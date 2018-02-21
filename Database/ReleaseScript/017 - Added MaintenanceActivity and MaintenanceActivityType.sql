create table dbo.MaintenanceActivityType(
MaintenanceActivityTypeID int not null constraint PK_MaintenanceActivityType_MaintenanceActivityTypeID primary key,
MaintenanceActivityTypeName varchar(30) not null constraint AK_MaintenanceActivityType_MaintenanceActivityTypeName unique,
MaintenanceActivityTypeDisplayName varchar(30) not null constraint AK_MaintenanceActivityType_MaintenanceActivityTypeDisplayName unique
)
go

create table dbo.MaintenanceActivity(
MaintenanceActivityID int not null identity(1,1) constraint PK_MaintenanceActivity_MaintenanceActivityID primary key,
TenantID int not null constraint FK_MaintenanceActivity_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
TreatmentBMPID int not null constraint FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
MaintenanceActivityDate date not null,
PerformedByPersonID int not null constraint FK_MaintenanceActivity_Person_PerformedByPersonID_PersonID foreign key references dbo.Person(PersonID),
MaintenanceActivityDescription varchar(500) null,
MaintenanceActivityTypeID int not null constraint FK_MaintenanceActivity_MaintenanceActivityType_MaintenanceActivityTypeID foreign key references dbo.MaintenanceActivityType(MaintenanceActivityTypeID),
constraint AK_MaintenanceActivity_MaintenanceActivityID_TenantID unique nonclustered (MaintenanceActivityID, TenantID),
constraint FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID_TenantID foreign key (TreatmentBMPID, TenantID) references dbo.TreatmentBMP(TreatmentBMPID, TenantID)
)
go

insert into dbo.FieldDefinition(FieldDefinitionID, FieldDefinitionName, FieldDefinitionDisplayName, DefaultDefinition, CanCustomizeLabel)
values (41, N'MaintenanceActivityType', N'Maintenance Activity Type', 'Whether the maintenance performed was Preventative or Corrective maintenance', 1),
(42, N'MaintenanceActivity', N'Maintenance Activity', 'A maintenance activity performed on a Treatment BMP', 1)