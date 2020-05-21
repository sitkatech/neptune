create table dbo.NereidResult (
	NereidResultID int not null identity(1,1) constraint PK_NereidResult_NereidResultID Primary Key,
	TreatmentBMPID int null,
	WaterQualityManagementPlanID int null,
	RegionalSubbasinID int null,
	DelineationID int null,
	NodeID varchar(max) null,
	FullResponse varchar(max) not null
	--,SubsequentSolveInputResponse varbinary(max) not null,
	--ReadOnlyResponseSubset varbinary(max) not null,
	--WebGridResponseSubset varbinary(max) not null
)