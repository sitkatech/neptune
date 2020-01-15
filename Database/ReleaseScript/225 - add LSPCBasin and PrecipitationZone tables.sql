create table dbo.LSPCBasinStaging
(
	LSPCBasinStagingID int not null identity(1,1) constraint PK_LSPCBasinStaging_LSPCBasinStagingID primary key,
	LSPCBasinKey int not null constraint AK_LSPCBasinStaging_LSPCBasinKey unique,
	LSPCBasinName varchar(100) not null,
	LSPCBasinGeometry geometry not null
)

create table dbo.LSPCBasin
(
	LSPCBasinID int not null identity(1,1) constraint PK_LSPCBasin_LSPCBasinID primary key,
	LSPCBasinKey int not null constraint AK_LSPCBasin_LSPCBasinKey unique,
	LSPCBasinName varchar(100) not null,
	LSPCBasinGeometry geometry not null,
	LastUpdate datetime not null
)


create table dbo.PrecipitationZoneStaging
(
	PrecipitationZoneStagingID int not null identity(1,1) constraint PK_PrecipitationZoneStaging_PrecipitationZoneStagingID primary key,
	PrecipitationZoneKey int not null constraint AK_PrecipitationZoneStaging_PrecipitationZoneKey unique,
	DesignStormwaterDepthInInches float not null,
	PrecipitationZoneGeometry geometry not null
)

create table dbo.PrecipitationZone
(
	PrecipitationZoneID int not null identity(1,1) constraint PK_PrecipitationZone_PrecipitationZoneID primary key,
	PrecipitationZoneKey int not null constraint AK_PrecipitationZone_PrecipitationZoneKey unique,
	DesignStormwaterDepthInInches float not null,
	PrecipitationZoneGeometry geometry not null,
	LastUpdate datetime not null
)

alter table dbo.TreatmentBMP add WatershedID int null constraint FK_TreatmentBMP_Watershed_WatershedID foreign key references dbo.Watershed(WatershedID)
alter table dbo.TreatmentBMP add LSPCBasinID int null constraint FK_TreatmentBMP_LSPCBasin_LSPCBasinID foreign key references dbo.LSPCBasin(LSPCBasinID)
alter table dbo.TreatmentBMP add PrecipitationZoneID int null constraint FK_TreatmentBMP_PrecipitationZone_PrecipitationZoneID foreign key references dbo.PrecipitationZone(PrecipitationZoneID)
GO

update t
set t.WatershedID = l.WatershedID
from dbo.TreatmentBMP t
left join dbo.Watershed l on t.LocationPoint.STIntersects(l.WatershedGeometry) = 1

INSERT INTO dbo.geometry_columns(f_table_catalog, f_table_schema, f_table_name, f_geometry_column, coord_dimension, srid, geometry_type)
VALUES 
('Neptune', 'dbo', 'LSPCBasin', 'LSPCBasinGeometry', 2, 2771, 'POLYGON'),
('Neptune', 'dbo', 'LSPCBasinStaging', 'LSPCBasinGeometry', 2, 2771, 'POLYGON'),
('Neptune', 'dbo', 'PrecipitationZone', 'PrecipitationZoneGeometry', 2, 2771, 'POLYGON'),
('Neptune', 'dbo', 'PrecipitationZoneStaging', 'PrecipitationZoneGeometry', 2, 2771, 'POLYGON')

