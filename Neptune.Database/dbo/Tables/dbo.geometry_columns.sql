CREATE TABLE [dbo].[geometry_columns](
	[f_table_catalog] [varchar](128),
	[f_table_schema] [varchar](128),
	[f_table_name] [varchar](256),
	[f_geometry_column] [varchar](256),
	[coord_dimension] [int] NOT NULL,
	[srid] [int] NOT NULL,
	[geometry_type] [varchar](30),
 CONSTRAINT [PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column] PRIMARY KEY CLUSTERED 
(
	[f_table_catalog] ASC,
	[f_table_schema] ASC,
	[f_table_name] ASC,
	[f_geometry_column] ASC
)
)
