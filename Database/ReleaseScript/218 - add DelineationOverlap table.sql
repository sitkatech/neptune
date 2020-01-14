create table dbo.DelineationOverlap
(
	DelineationOverlapID int not null constraint PK_DelineationOverlap_DelineationOverlapID primary key,
	DelineationID int not null constraint FK_DelineationOverlap_Delineation_DelineationID foreign key references dbo.Delineation(DelineationID),
	OverlappingDelineationID int not null constraint FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID foreign key references dbo.Delineation(DelineationID),
	OverlappingGeometry geometry not null
)
