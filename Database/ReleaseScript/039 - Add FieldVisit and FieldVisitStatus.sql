create table dbo.FieldVisitStatus(
FieldVisitStatusID int not null constraint PK_FieldVisitStatus_FieldVisitStatusID primary key,
FieldVisitStatusName varchar(20) not null constraint AK_FieldVisitStatus_FieldVisitStatusName unique,
FieldVisitStatusDisplayName varchar(20) not null )

insert into dbo.FieldVisitStatus(FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
values
(1, 'InProgress', 'In Progress'),
(2, 'Complete', 'Complete'),
(3, 'Unresolved', 'Unresolved')

create table dbo.FieldVisit(
FieldVisitID int not null identity(1,1) constraint PK_FieldVisit_FieldVisitID primary key,
TenantID int not null constraint FK_FieldVisit_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
FieldVisitStatusID int null constraint FK_FieldVisit_FieldVisitStatus_FieldVisitStatusID foreign key references dbo.FieldVisitStatus(FieldVisitStatusID),
InitialAssessmentID int null constraint FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPAssessmentID foreign key references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID),
MaintenanceRecordID int null constraint FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID foreign key references dbo.MaintenanceRecord(MaintenanceRecordID),
PostMaintenanceAssessmentID int null constraint FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID foreign key references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID),
PerformedByPersonID int not null constraint FK_FieldVisit_Person_PerformedByPersonID_PersonID foreign key references dbo.Person(PersonID),
VisitDate datetime not null
)

 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
 	foreign key (InitialAssessmentID, TenantID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TenantID
 	foreign key (MaintenanceRecordID, TenantID) references dbo.MaintenanceRecord(MaintenanceRecordID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
 	foreign key (PostMaintenanceAssessmentID, TenantID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_Person_PerformedByPersonID_TenantID_PersonID_TenantID
 	foreign key (PerformedByPersonID, TenantID) references dbo.Person(PersonID, TenantID)