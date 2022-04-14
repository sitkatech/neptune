create table dbo.OCTAPrioritization (
	OCTAPrioritizationID int not null identity(1,1) constraint PK_OCTAPrioritization_OCTAPrioritizationID primary key,
	OCTAPrioritizationKey int not null constraint AK_OCTAPrioritization_OCTAPrioritizationKey unique,
	OCTAPrioritizationGeometry geometry not null,
	OCTAPrioritizationGeometry4326 geometry null,
	LastUpdate datetime not null,
	Watershed varchar(80) not null,
	CatchIDN varchar(80) not null,
	TPI float not null,
	WQNLU float not null,
	WQNMON float not null,
	IMPAIR float not null,
	MON float not null,
	SEA float not null,
	SEA_PCTL varchar(80) not null,
	PC_VOL_PCT float not null,
	PC_NUT_PCT float not null,
	PC_BAC_PCT float not null,
	PC_MET_PCT float not null,
	PC_TSS_PCT float not null
)

create table dbo.OCTAPrioritizationStaging (
	OCTAPrioritizationStagingID int not null identity(1,1) constraint PK_OCTAPrioritizationStaging_OCTAPrioritizationStagingID primary key,
	OCTAPrioritizationKey int not null constraint AK_OCTAPrioritizationStaging_OCTAPrioritizationKey unique,
	OCTAPrioritizationGeometry geometry not null,
	Watershed varchar(80) not null,
	CatchIDN varchar(80) not null,
	TPI float not null,
	WQNLU float not null,
	WQNMON float not null,
	IMPAIR float not null,
	MON float not null,
	SEA float not null,
	SEA_PCTL varchar(80) not null,
	PC_VOL_PCT float not null,
	PC_NUT_PCT float not null,
	PC_BAC_PCT float not null,
	PC_MET_PCT float not null,
	PC_TSS_PCT float not null
)

insert into dbo.geometry_columns([f_table_catalog]
      ,[f_table_schema]
      ,[f_table_name]
      ,[f_geometry_column]
      ,[coord_dimension]
      ,[srid]
      ,[geometry_type])
values ('Neptune', 'dbo', 'OCTAPrioritizationStaging', 'OCTAPrioritizationGeometry', 2, 2771, 'POLYGON'),
('Neptune', 'dbo', 'OCTAPrioritization', 'OCTAPrioritizationGeometry', 2, 2771, 'POLYGON')