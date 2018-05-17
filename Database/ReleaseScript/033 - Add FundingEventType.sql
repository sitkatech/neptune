create table dbo.FundingEventType(
FundingEventTypeID int not null constraint PK_FundingEventType_FundingEventTypeID primary key,
FundingEventTypeName varchar(100) not null constraint AK_FundingEventType_FundingEventTypeName unique,
FundingEventTypeDisplayName varchar(100) not null
)
go

insert into dbo.FundingEventType (FundingEventTypeID, FundingEventTypeName, FundingEventTypeDisplayName) values
(1, 'PlanningAndDesign', 'Planning & Design'),
(2, 'CapitalConstruction', 'Capital Construction'),
(3, 'RehabilitativeMaintenance', 'Rehabilitative Maintenance'),
(4, 'RoutineMaintenance', 'Routine Maintenance'),
(5, 'Retrofit', 'Retrofit')