create table dbo.OperationMonth(
OperationMonthID int not null constraint PK_OperationMonth_OperationMonthID primary key,
OperationMonthName varchar(6) not null constraint AK_OperationMonth_OperationMonthName unique,
OperationMonthDisplayName varchar(6) not null constraint AK_OperationMonth_OperationMonthDisplayName unique,
OperationMonthNereidAlias varchar(6) not null
)

Insert into dbo.OperationMonth (OperationMonthID, OperationMonthName, OperationMonthDisplayName, OperationMonthNereidAlias)
values
(1, 'Summer', 'Summer', 'summer'),
(2, 'Winter', 'Winter', 'winter'),
(3, 'Both', 'Both', 'both')

go

Alter table dbo.TreatmentBMPModelingAttribute
Add OperationMonthID int null constraint FK__TreatmentBMPModelingAttribute_OperationMonth_OperationMonthID
	foreign key references dbo.OperationMonth(OperationMonthID)
GO

--Set the summer months
Update ma
set ma.OperationMonthID = 1
from dbo.TreatmentBMPModelingAttribute ma
	inner join dbo.TreatmentBMPOperationMonth mo
	on ma.TreatmentBMPID = mo.TreatmentBMPID
where mo.OperationMonth = 4

-- set the winter/both months

Update ma
set ma.OperationMonthID = 
	case
		when ma.OperationMonthID is not null then 3
		else 2
	end
from dbo.TreatmentBMPModelingAttribute ma
	inner join dbo.TreatmentBMPOperationMonth mo
	on ma.TreatmentBMPID = mo.TreatmentBMPID
where mo.OperationMonth = 10
GO

Drop table TreatmentBMPOperationMonth