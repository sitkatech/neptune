Create table dbo.RegionalSubbasinRevisionRequestStatus(
RegionalSubbasinRevisionRequestStatusID int not null constraint PK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID primary key,
RegionalSubbasinRevisionRequestStatusName varchar(20) not null constraint AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusName unique,
RegionalSubbasinRevisionRequestStatusDisplayName varchar(20) not null constraint AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusDisplayName unique
)

Create table dbo.RegionalSubbasinRevisionRequest (
RegionalSubbasinRevisionRequestID int not null identity(1,1) constraint PK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestID primary key,
TreatmentBMPID int not null constraint PK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID
	foreign key references dbo.TreatmentBMP(TreatmentBMPID),
RegionalSubbasinRevisionRequestGeometry geometry not null,
RequestPersonID int not null constraint FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID
	foreign key references dbo.Person(PersonID),
RegionalSubbasinRevisionRequestStatusID int not null constraint FK_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID
	foreign key references dbo.RegionalSubbasinRevisionRequestStatus(RegionalSubbasinRevisionRequestStatusID),
RequestDate datetime not null,
ClosedByPersonID int null constraint FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID
	foreign key references dbo.Person(PersonID),
ClosedDate datetime null,
Notes varchar(max) null,
CloseNotes varchar(max) null
)