 create table dbo.LandUseBlockGeomteryStaging (
 LandUseBlockStagingID int identity(1,1) not null constraint PK_LandUseBlockStaging_LandUseBlockStagingID primary key,
 PersonID int not null constraint FK_LandUseBlockStaging_Person_PersonID foreign key references dbo.Person (PersonID),
 FeatureClassName varchar(255) not null,
 LandUseBlockStagingGeoJson varchar(max) not null,
 SelectedProperty varchar(255) null,
 ShouldImport bit not null)