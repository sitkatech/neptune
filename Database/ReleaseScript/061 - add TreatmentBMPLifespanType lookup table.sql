create table dbo.TreatmentBMPLifespanType (
TreatmentBMPLifespanTypeID int not null constraint PK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeID primary key,
TreatmentBMPLifespanTypeName varchar(50) not null constraint AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeName unique,
TreatmentBMPLifespanTypeDisplayName varchar(50) not null constraint AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeDisplayName unique
)
go

insert into dbo.TreatmentBMPLifespanType (TreatmentBMPLifespanTypeID, TreatmentBMPLifespanTypeName, TreatmentBMPLifespanTypeDisplayName)
values
(1, 'Unspecified', 'Unspecified/Voluntary'),
(2, 'Perpetuity', 'Perpetuity/Life of Project'),
(3, 'FixedEndDate', 'Fixed End Date')