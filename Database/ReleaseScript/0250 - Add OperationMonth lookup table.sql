create table dbo.MonthsOfOperation(
MonthsOfOperationID int not null constraint PK_MonthsOfOperation_MonthsOfOperationID primary key,
MonthsOfOperationName varchar(6) not null constraint AK_MonthsOfOperation_MonthsOfOperationName unique,
MonthsOfOperationDisplayName varchar(6) not null constraint AK_MonthsOfOperation_MonthsOfOperationDisplayName unique,
MonthsOfOperationNereidAlias varchar(6) not null
)

Insert into dbo.MonthsOfOperation (MonthsOfOperationID, MonthsOfOperationName, MonthsOfOperationDisplayName, MonthsOfOperationNereidAlias)
values
(1, 'Summer', 'Summer', 'summer'),
(2, 'Winter', 'Winter', 'winter'),
(3, 'Both', 'Both', 'both')

go

Alter table dbo.TreatmentBMPModelingAttribute
Add MonthsOfOperationID int null constraint FK__TreatmentBMPModelingAttribute_MonthsOfOperation_MonthsOfOperationID
	foreign key references dbo.MonthsOfOperation(MonthsOfOperationID)
GO

--Set the summer months
Update ma
set ma.MonthsOfOperationID = 1
from dbo.TreatmentBMPModelingAttribute ma
	inner join dbo.TreatmentBMPOperationMonth mo
	on ma.TreatmentBMPID = mo.TreatmentBMPID
where mo.OperationMonth = 4

-- set the winter/both months

Update ma
set ma.MonthsOfOperationID = 
	case
		when ma.MonthsOfOperationID is not null then 3
		else 2
	end
from dbo.TreatmentBMPModelingAttribute ma
	inner join dbo.TreatmentBMPOperationMonth mo
	on ma.TreatmentBMPID = mo.TreatmentBMPID
where mo.OperationMonth = 11
GO

Drop table dbo.TreatmentBMPOperationMonth