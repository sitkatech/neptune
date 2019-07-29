Insert into geometry_columns([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type])
values
('Neptune', 'dbo', 'TrashGeneratingUnit', 'TrashGeneratingUnitGeometry', 2, 4326, 'MULTIPOLYGON')

alter table dbo.TrashGeneratingUnit
Add Constraint DF_LastUpdateDate Default GetDate() for LastUpdateDate