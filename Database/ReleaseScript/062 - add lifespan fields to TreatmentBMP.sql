alter table dbo.TreatmentBMP
add TreatmentBMPLifespanTypeID int null
go

alter table dbo.TreatmentBMP
add TreatmentBMPLifespanEndDate datetime null
go

alter table dbo.TreatmentBMP
add constraint FK_TreatmentBMP_TreatmentBMPLifespanType foreign key (TreatmentBMPLifespanTypeID) references dbo.TreatmentBMPLifespanType(TreatmentBMPLifespanTypeID)

alter table dbo.TreatmentBMP
add constraint CK_TreatmentBMP_LifespanEndDateMustBeSetIfLifespanTypeIsFixedEndDate
check ((TreatmentBMPLifespanTypeID = 3 AND TreatmentBMPLifespanEndDate is not null) OR (TreatmentBMPLifespanTypeID != 3 AND TreatmentBMPLifespanEndDate is null))