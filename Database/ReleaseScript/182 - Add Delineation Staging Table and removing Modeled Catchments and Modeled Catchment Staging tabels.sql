create table dbo.DelineationGeometryStaging (
DelineationGeometryStagingID int not null identity(1,1) constraint PK_DelineationStagging_DelineationStagingID primary key,
DelineationGeometryStagingGeometry varchar(max) not null,
PersonID int not null constraint FK_DelineationStaging_Person_PersonID foreign key references dbo.Person (personID),
FeatureClassName varchar(255) not null,
SelectedProperty varchar(255) null,
ShouldImport bit not null
)

--go

--drop table ModeledCatchment

--go

--drop table ModeledCatchmentGeometryStaging