create table dbo.NereidResult (
	NereidResultID int not null identity(1,1) constraint PK_NereidResult_NereidResultID Primary Key,
	TreatmentBMPID int,
	WaterQualityManagementPlanID int,
	RegionalSubbasinID int,
	DelineationID int,
	NodeID varchar(max),
	FullResponse varbinary(max) not null
	--,SubsequentSolveInputResponse varbinary(max) not null,
	--ReadOnlyResponseSubset varbinary(max) not null,
	--WebGridResponseSubset varbinary(max) not null
)