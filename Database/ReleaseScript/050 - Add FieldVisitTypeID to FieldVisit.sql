alter table dbo.FieldVisit
add FieldVisitTypeID int null constraint FK_FieldVisit_FieldVisitType_FieldVisitTypeID foreign key references dbo.FieldVisitType(FieldVisitTypeID)
go

update dbo.FieldVisit
set FieldVisitTypeID = 1

alter table dbo.FieldVisit
alter column FieldVisitTypeID int not null
