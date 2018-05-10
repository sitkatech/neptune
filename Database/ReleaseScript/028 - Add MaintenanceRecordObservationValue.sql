create table dbo.MaintenanceRecordObservationValue (
MaintenanceRecordObservationValueID int not null identity(1,1)
        constraint PK_MaintenanceRecordObservationValue_MaintenanceRecordObservationValueID primary key,
TenantID int not null
        constraint FK_MaintenanceRecordObservationValue_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
​MaintenanceRecordObservationID int not null
        constraint FK_MaintenanceRecordObservationValue_MaintenanceRecordObservation_MaintenanceRecordObservationID
        foreign key references dbo.MaintenanceRecordObservation(MaintenanceRecordObservationID),
ObservationValue varchar(1000) not null
)