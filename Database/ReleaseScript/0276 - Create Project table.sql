create table dbo.ProjectStatus(
	ProjectStatusID int not null constraint PK_ProjectStatus_ProjectStatusID primary key,
	ProjectStatusName varchar(50) not null constraint AK_ProjectStatus_ProjectStatusName unique,
	ProjectStatusDisplayName varchar(50) not null constraint AK_ProjectStatus_ProjectStatusDisplayName unique,
	ProjectStatusSortOrder int not null
)

create table dbo.Project(
	ProjectID int not null identity(1,1) constraint PK_Project_ProjectID primary key,
	ProjectName varchar(200) not null constraint AK_Project_ProjectName unique,
	OrganizationID int not null constraint FK_Project_Organization_OrganizationID foreign key references dbo.Organization(OrganizationID),
	StormwaterJurisdictionID int not null constraint FK_Project_StormwaterJurisdiction_StormwaterJurisdictionID foreign key references dbo.StormwaterJurisdiction(StormwaterJurisdictionID),
	ProjectStatusID int not null constraint FK_Project_ProjectStatus_ProjectStatusID foreign key references dbo.ProjectStatus(ProjectStatusID),
	PrimaryContactPersonID int not null constraint FK_Project_Person_PrimaryContactPersonID_PersonID foreign key references dbo.Person(PersonID),
	CreatePersonID int not null constraint FK_Project_Person_CreatePersonID_PersonID foreign key references dbo.Person(PersonID),
	DateCreated datetime not null,
	ProjectDescription varchar(500) null,
	AdditionalContactInformation varchar(500) null
) 