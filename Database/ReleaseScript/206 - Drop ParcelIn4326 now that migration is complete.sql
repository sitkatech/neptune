Drop Table if Exists dbo.ParcelIn4326

if not exists( select * from dbo.geometry_columns where f_geometry_column ='TrashGeneratingUnitGeometry' and f_table_name = 'TrashGeneratingUnit')
Insert into dbo.geometry_columns values
('Neptune', 'dbo', 'TrashGeneratingUnit', 'TrashGeneratingUnitGeometry', 2, 2771, 'MULTIPOLYGON')