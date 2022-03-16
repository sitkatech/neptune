--Not totally sure why most of these are not FKs, but aiming to essentially copy the nereid result table
create table dbo.PlannedProjectNereidResult (
	PlannedProjectNereidResultID int not null identity(1,1) constraint PK_PlannedProjectNereidResult_PlannedProjectNereidResultID primary key,
	ProjectID int not null constraint FK_PlannedProjectNereidResult_Project_ProjectID foreign key references dbo.Project (ProjectID),
	IsBaselineCondition bit not null,
	TreatmentBMPID int null,
	WaterQualityManagementPlanID int null,
	RegionalSubbasinID int null,
	NodeID varchar(max) null,
	FullResponse varchar(max) null,
	LastUpdate datetime null
)