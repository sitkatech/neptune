exec sp_rename 'PK_DelineationStaging_DelineationStagingID', 'PK_DelineationGeometryStaging_DelineationGeometryStagingID', 'OBJECT'


CREATE TABLE dbo.DelineationStaging(
	DelineationStagingID int IDENTITY(1,1) NOT NULL,
	DelineationStagingGeometry geometry NOT NULL,
	IsVerified bit NOT NULL,
	DateLastVerified datetime NULL,
	UploadedByPersonID int NULL,
	TreatmentBMPName varchar(200) NOT NULL,
	DateLastModified datetime NOT NULL,
	StormwaterJurisdictionID int not null
 CONSTRAINT PK_DelineationStaging_DelineationStagingID PRIMARY KEY CLUSTERED 
(
	DelineationStagingID 
),
 CONSTRAINT AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID UNIQUE NONCLUSTERED 
(
	TreatmentBMPName, StormwaterJurisdictionID
)
)
GO

ALTER TABLE dbo.DelineationStaging  WITH CHECK ADD  CONSTRAINT FK_DelineationStaging_Person_UploadedByPersonID_PersonID FOREIGN KEY(UploadedByPersonID)
REFERENCES dbo.Person (PersonID)
GO

ALTER TABLE dbo.DelineationStaging  WITH CHECK ADD  CONSTRAINT FK_DelineationStaging_StormwaterJurisdiction_StormwaterJurisdictionID FOREIGN KEY(StormwaterJurisdictionID)
REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID)
GO


 insert into dbo.geometry_columns (f_table_catalog, f_table_schema, f_table_name, f_geometry_column, coord_dimension, srid, geometry_type)
  values ('Neptune', 'dbo', 'DelineationStaging', 'DelineationStagingGeometry', 2, 4326, 'POLYGON')