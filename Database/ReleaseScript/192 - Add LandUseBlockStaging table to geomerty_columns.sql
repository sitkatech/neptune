CREATE TABLE dbo.geometry_columns(
	f_table_catalog varchar(128) NOT NULL,
	f_table_schema varchar(128) NOT NULL,
	f_table_name varchar(256) NOT NULL,
	f_geometry_column varchar(256) NOT NULL,
	coord_dimension int NOT NULL,
	srid int NOT NULL,
	geometry_type varchar(30) NOT NULL,

	Constraint PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column
		PRIMARY KEY
		( f_table_catalog, f_table_schema, f_table_name)
)

GO

 insert into dbo.geometry_columns (f_table_catalog, f_table_schema, f_table_name, f_geometry_column, coord_dimension, srid, geometry_type)
  values ('Neptune', 'dbo', 'LandUseBlockStaging', 'LandUseBlockStagingGeometry', 2, 4326, 'MULTIPOLYGON')

GO

CREATE TABLE dbo.spatial_ref_sys(
	srid int NOT NULL  CONSTRAINT PK_spatial_ref_sys_srid PRIMARY KEY CLUSTERED,
	auth_name varchar(256) NULL,
	auth_srid int NULL,
	srtext varchar(2048) NULL,
	proj4text varchar(2048) NULL
)

insert into dbo.spatial_ref_sys (srid, auth_name, auth_srid, srtext, proj4text)
values (4326, 'ESPG',4326, 'GEOGCS"WGS 84",DATUM"WGS_1984",SPHEROID"WGS 84",6378137,298.257223563,AUTHORITY"EPSG","7030",AUTHORITY"EPSG","6326",PRIMEM"Greenwich",0,AUTHORITY"EPSG","8901",UNIT"degree",0.0174532925199433,AUTHORITY"EPSG","9122",AUTHORITY"EPSG","4326"', '+proj=longlat +datum=WGS84 +no_defs ')