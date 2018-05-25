create table dbo.FieldVisitStatus(
FieldVisitStatusID int not null constraint PK_FieldVisitStatus_FieldVisitStatusID primary key,
FieldVisitStatusName varchar(20) not null constraint AK_FieldVisitStatus_FieldVisitStatusName unique,
FieldVisitStatusDisplayName varchar(20) not null )

insert into dbo.FieldVisitStatus(FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
values
(1, 'InProgress', 'In Progress'),
(2, 'Complete', 'Complete'),
(3, 'Unresolved', 'Unresolved')

create table dbo.FieldVisitSection(
FieldVisitSectionID int not null constraint PK_FieldVisitSection_FieldVisitSectionID primary key,
FieldVisitSectionName varchar(50) not null constraint AK_FieldVisitSection_FieldVisitSectionName unique,
FieldVisitSectionDisplayName varchar(50) not null,
SectionHeader varchar(100) not null,
SortOrder int not null )

insert into dbo.FieldVisitSection(FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SectionHeader, SortOrder)
values
(1, 'Inventory', 'Inventory', 'Review and Update Inventory?', 10),
(2, 'Assessment', 'Assessment', 'Assessment', 20),
(3, 'Maintenance', 'Maintenance', 'Maintenance', 30),
(4, 'PostMaintenanceAssessment', 'Post-Maintenance Assessment', 'Post-Maintenance Assessment', 40),
(5, 'WrapUpVisit', 'Wrap-up Visit', 'Wrap-up Visit', 50),
(6, 'ManageVisit', 'Manage Visit', 'Manage Visit', 60)

create table dbo.FieldVisit(
FieldVisitID int not null identity(1,1) constraint PK_FieldVisit_FieldVisitID primary key,
TenantID int not null constraint FK_FieldVisit_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
TreatmentBMPID int not null constraint FK_FieldVisit_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
FieldVisitStatusID int not null constraint FK_FieldVisit_FieldVisitStatus_FieldVisitStatusID foreign key references dbo.FieldVisitStatus(FieldVisitStatusID),
InitialAssessmentID int null constraint FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPAssessmentID foreign key references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID),
MaintenanceRecordID int null constraint FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID foreign key references dbo.MaintenanceRecord(MaintenanceRecordID),
PostMaintenanceAssessmentID int null constraint FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID foreign key references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID),
PerformedByPersonID int not null constraint FK_FieldVisit_Person_PerformedByPersonID_PersonID foreign key references dbo.Person(PersonID),
VisitDate datetime not null
)

-- double keys for tenant ID
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
 	foreign key (InitialAssessmentID, TenantID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TenantID
 	foreign key (MaintenanceRecordID, TenantID) references dbo.MaintenanceRecord(MaintenanceRecordID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
 	foreign key (PostMaintenanceAssessmentID, TenantID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TenantID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_Person_PerformedByPersonID_TenantID_PersonID_TenantID
 	foreign key (PerformedByPersonID, TenantID) references dbo.Person(PersonID, TenantID)
Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMP_TreatmentBMPID_TenantID foreign key (TreatmentBMPID, TenantID) references dbo.TreatmentBMP(TreatmentBMPID, TenantID)

--double keys for consistency
Alter Table dbo.TreatmentBMPAssessment Add Constraint AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID unique(TreatmentBMPAssessmentID, TreatmentBMPID)
Alter Table dbo.MaintenanceRecord Add Constraint AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID unique(MaintenanceRecordID, TreatmentBMPID)

 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID
 	foreign key (InitialAssessmentID, TreatmentBMPID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TreatmentBMPID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID
 	foreign key (MaintenanceRecordID, TreatmentBMPID) references dbo.MaintenanceRecord(MaintenanceRecordID, TreatmentBMPID)
 Alter Table dbo.FieldVisit Add Constraint FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID
 	foreign key (PostMaintenanceAssessmentID, TreatmentBMPID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TreatmentBMPID)