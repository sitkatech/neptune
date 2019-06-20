Drop Table dbo.DelineationGeometryStaging
GO

CREATE TABLE dbo.DelineationStaging(
	DelineationStagingID int IDENTITY(1,1) NOT NULL,
	DelineationStagingGeometry geometry NOT NULL,
	UploadedByPersonID int not NULL,
	TreatmentBMPName varchar(200) not null,
	StormwaterJurisdictionID int not null
 CONSTRAINT PK_DelineationStaging_DelineationStagingID PRIMARY KEY CLUSTERED 
(
	DelineationStagingID 
), Constraint AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID unique
(
	TreatmentBMPName, StormwaterJurisdictionID
)
)
GO

ALTER TABLE dbo.DelineationStaging  WITH CHECK ADD  CONSTRAINT FK_DelineationStaging_Person_UploadedByPersonID_PersonID FOREIGN KEY(UploadedByPersonID)
REFERENCES dbo.Person (PersonID)
GO

 insert into dbo.geometry_columns (f_table_catalog, f_table_schema, f_table_name, f_geometry_column, coord_dimension, srid, geometry_type)
  values ('Neptune', 'dbo', 'DelineationStaging', 'DelineationStagingGeometry', 2, 4326, 'POLYGON')
