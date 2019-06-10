create table dbo.DelineationGeometryStaging (
DelineationGeometryStagingID int not null identity(1,1) constraint PK_DelineationStagging_DelineationStagingID primary key,
PersonID int not null constraint FK_DelineationStaging_Person_PersonID foreign key references dbo.Person (personID),
FeatureClassName varchar(255) not null,
DelineationGeometryStagingGeometry varchar(max) not null,
SelectedProperty varchar(255) null,
ShouldImport bit not null
)
GO

Alter Table dbo.TreatmentBMP
Drop Constraint FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID
ALTER TABLE [dbo].[ModeledCatchment] DROP CONSTRAINT [FK_ModeledCatchment_StormwaterJurisdiction_StormwaterJurisdictionID]
drop table ModeledCatchment
drop table ModeledCatchmentGeometryStaging
