create table dbo.ProjectNetworkSolveHistoryStatusType (
	ProjectNetworkSolveHistoryStatusTypeID int not null constraint PK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID primary key,
	ProjectNetworkSolveHistoryStatusTypeName varchar(50) not null constraint AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeName unique,
	ProjectNetworkSolveHistoryStatusTypeDisplayName varchar(50) not null constraint AK_ProjectNetworkSolveHistoryStatusTypeProjectNetworkSolveHistoryStatusTypeDisplayName unique
)

insert into dbo.ProjectNetworkSolveHistoryStatusType (ProjectNetworkSolveHistoryStatusTypeID, ProjectNetworkSolveHistoryStatusTypeName, ProjectNetworkSolveHistoryStatusTypeDisplayName)
values (1, 'Queued', 'Queued'),
(2, 'Succeeded', 'Succeeded'),
(3, 'Failed', 'Failed')

create table dbo.ProjectNetworkSolveHistory (
	ProjectNetworkSolveHistoryID int not null identity(1,1) constraint PK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryID primary key,
	ProjectID int not null constraint FK_ProjectNetworkSolveHistory_Project_ProjectID foreign key references dbo.Project (ProjectID),
	RequestedByPersonID int not null constraint FK_ProjectNetworkSolveHistory_Person_RequestedByPersonID_PersonID foreign key references dbo.Person(PersonID),
	ProjectNetworkSolveHistoryStatusTypeID int not null constraint FK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID foreign key references dbo.ProjectNetworkSolveHistoryStatusType (ProjectNetworkSolveHistoryStatusTypeID),
	ErrorMessage dbo.html null
)