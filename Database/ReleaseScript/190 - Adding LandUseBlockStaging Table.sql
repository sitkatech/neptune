 create table dbo.LandUseBlockGeometryStaging (
 LandUseBlockGeometryStagingID int identity(1,1) not null constraint PK_LandUseBlockGeometryStaging_LandUseBlockGeometryStagingID primary key,
 PersonID int not null constraint FK_LandUseBlockGeometryStaging_Person_PersonID foreign key references dbo.Person (PersonID),
 StormwaterJurisdictionID int not null constraint FK_LandUseBlockGeometryStaging_StormwaterJurisdiction_StormwaterJurosdictionID foreign key references dbo.StormwaterJurisdiction (StormwaterJurisdictionID),
 FeatureClassName varchar(255) not null,
 LandUseBlockGeometryStagingGeoJson varchar(max) not null,
 SelectedProperty varchar(255) null,
 ShouldImport bit not null)