Create Table dbo.TrashCaptureStatusType(
TrashCaptureStatusTypeID int not null constraint PK_TrashCaptureStatusType_TrashCaptureStatusTypeID primary key,
TrashCaptureStatusTypeName varchar(50) not null constraint AK_TrashCaptureStatusType_TrashCaptureStatusTypeName unique,
TrashCaptureStatusTypeDisplayName varchar(50) not null constraint AK_TrashCaptureStatusType_TrashCaptureStatusTypeDisplayName unique,
TrashCaptureStatusTypeSortOrder int not null
)

insert into dbo.TrashCaptureStatusType (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder)
values
(1, 'Full', 'Full', 10),
(2, 'Partial', 'Partial', 20),
(3, 'None', 'None', 30),
(4, 'NotProvided', 'Not Provided', 40)

Alter table dbo.TreatmentBMP			
Add TrashCaptureStatusTypeID int null constraint FK_TreatmentBMP_TrashCaptureStatusType_TrashCaptureStatusTypeID foreign key references dbo.TrashCaptureStatusType(TrashCaptureStatusTypeID)
go

Update dbo.TreatmentBMP
set TrashCaptureStatusTypeID = 4

Alter table dbo.TreatmentBMP
alter column TrashCaptureStatusTypeID int not null