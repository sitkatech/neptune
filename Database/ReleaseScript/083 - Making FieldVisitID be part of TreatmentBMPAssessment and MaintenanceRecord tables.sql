-- delete orphaned MaintenanceRecords
delete mrov
from dbo.MaintenanceRecord mr
join dbo.MaintenanceRecordObservation mro on mr.MaintenanceRecordID = mro.MaintenanceRecordID
join dbo.MaintenanceRecordObservationValue mrov on mro.MaintenanceRecordObservationID = mrov.MaintenanceRecordObservationID
left join dbo.FieldVisit fv1 on mr.MaintenanceRecordID = fv1.MaintenanceRecordID
where fv1.FieldVisitID is null

delete mro
from dbo.MaintenanceRecord mr
join dbo.MaintenanceRecordObservation mro on mr.MaintenanceRecordID = mro.MaintenanceRecordID
left join dbo.FieldVisit fv1 on mr.MaintenanceRecordID = fv1.MaintenanceRecordID
where fv1.FieldVisitID is null

delete mr
from dbo.MaintenanceRecord mr
left join dbo.FieldVisit fv1 on mr.MaintenanceRecordID = fv1.MaintenanceRecordID
where fv1.FieldVisitID is null


create table dbo.TreatmentBMPAssessmentType
(
	TreatmentBMPAssessmentTypeID int not null constraint PK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID primary key,
	TreatmentBMPAssessmentTypeName varchar(50) not null constraint AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeName unique,
	TreatmentBMPAssessmentTypeDisplayName varchar(50) not null constraint AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeDisplayName unique
)

alter table dbo.FieldVisit add constraint AK_FieldVisit_FieldVisitID_TreatmentBMPID unique(FieldVisitID, TreatmentBMPID)

alter table dbo.TreatmentBMPAssessment add FieldVisitID int null, TreatmentBMPAssessmentTypeID int null
alter table dbo.TreatmentBMPAssessment add constraint FK_TreatmentBMPAssessment_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID foreign key (TreatmentBMPAssessmentTypeID) references dbo.TreatmentBMPAssessmentType(TreatmentBMPAssessmentTypeID)
alter table dbo.TreatmentBMPAssessment add constraint FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID foreign key (FieldVisitID) references dbo.FieldVisit(FieldVisitID)
alter table dbo.TreatmentBMPAssessment add constraint FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID_TreatmentBMPID foreign key (FieldVisitID, TreatmentBMPID) references dbo.FieldVisit(FieldVisitID, TreatmentBMPID)


alter table dbo.MaintenanceRecord add FieldVisitID int null, TreatmentBMPTypeID int null
alter table dbo.MaintenanceRecord add constraint FK_MaintenanceRecord_FieldVisit_FieldVisitID foreign key (FieldVisitID) references dbo.FieldVisit(FieldVisitID)
alter table dbo.MaintenanceRecord add constraint FK_MaintenanceRecord_FieldVisit_FieldVisitID_TreatmentBMPID foreign key (FieldVisitID, TreatmentBMPID) references dbo.FieldVisit(FieldVisitID, TreatmentBMPID)

ALTER TABLE dbo.FieldVisit DROP CONSTRAINT CK_InitialAssessmentMustBeDifferentFromPMAssessmentIfNotBothNull
DROP INDEX AK_FieldVisit_InitialAssessmentID ON dbo.FieldVisit
DROP INDEX AK_FieldVisit_MaintenanceRecordID ON dbo.FieldVisit
DROP INDEX AK_FieldVisit_PostMaintenanceAssessmentID ON dbo.FieldVisit
GO

insert into dbo.TreatmentBMPAssessmentType(TreatmentBMPAssessmentTypeID, TreatmentBMPAssessmentTypeName, TreatmentBMPAssessmentTypeDisplayName)
values
(1, 'Initial', 'Initial'),
(2, 'PostMaintenance', 'Post-Maintenance')

update tba
set FieldVisitID = fv.FieldVisitID, TreatmentBMPAssessmentTypeID = 1
from dbo.TreatmentBMPAssessment tba
join dbo.FieldVisit fv on tba.TreatmentBMPAssessmentID = fv.InitialAssessmentID

update tba
set FieldVisitID = fv.FieldVisitID, TreatmentBMPAssessmentTypeID = 2
from dbo.TreatmentBMPAssessment tba
join dbo.FieldVisit fv on tba.TreatmentBMPAssessmentID = fv.PostMaintenanceAssessmentID


update mr
set FieldVisitID = fv.FieldVisitID
from dbo.MaintenanceRecord mr
join dbo.FieldVisit fv on mr.MaintenanceRecordID = fv.MaintenanceRecordID

update mr
set TreatmentBMPTypeID = tb.TreatmentBMPTypeID
from dbo.MaintenanceRecord mr
join dbo.TreatmentBMP tb on mr.TreatmentBMPID = tb.TreatmentBMPID



alter table dbo.TreatmentBMPAssessment alter column FieldVisitID int not null
alter table dbo.TreatmentBMPAssessment alter column TreatmentBMPAssessmentTypeID int not null
alter table dbo.MaintenanceRecord alter column FieldVisitID int not null
alter table dbo.MaintenanceRecord alter column TreatmentBMPTypeID int not null
alter table dbo.MaintenanceRecord add constraint FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID foreign key (TreatmentBMPID, TreatmentBMPTypeID) references dbo.TreatmentBMP (TreatmentBMPID, TreatmentBMPTypeID)
alter table dbo.MaintenanceRecord add constraint AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID unique (MaintenanceRecordID, TreatmentBMPTypeID)
ALTER TABLE dbo.MaintenanceRecordObservation ADD  CONSTRAINT FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPTypeID FOREIGN KEY(MaintenanceRecordID, TreatmentBMPTypeID) REFERENCES dbo.MaintenanceRecord (MaintenanceRecordID, TreatmentBMPTypeID)

alter table dbo.MaintenanceRecordObservation add constraint FK_MaintenanceRecordObservation_MaintenanceRecordID_TreatmentBMPTypeID foreign key (MaintenanceRecordID, TreatmentBMPTypeID) references dbo.MaintenanceRecord (MaintenanceRecordID, TreatmentBMPTypeID)

ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPAssessmentID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_InitialAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID

ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TenantID_TreatmentBMPAssessmentID_TenantID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPAssessmentID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_TreatmentBMPAssessment_PostMaintenanceAssessmentID_TreatmentBMPID_TreatmentBMPAssessmentID_TreatmentBMPID

ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TenantID
ALTER TABLE dbo.FieldVisit DROP CONSTRAINT FK_FieldVisit_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID

alter table dbo.FieldVisit drop column InitialAssessmentID
alter table dbo.FieldVisit drop column PostMaintenanceAssessmentID
alter table dbo.FieldVisit drop column MaintenanceRecordID

alter table dbo.TreatmentBMPAssessment add constraint AK_TreatmentBMPAssessment_FieldVisitID_TreatmentBMPAssessmentTypeID unique (FieldVisitID, TreatmentBMPAssessmentTypeID)
alter table dbo.MaintenanceRecord add constraint AK_MaintenanceRecord_FieldVisitID unique (FieldVisitID)
