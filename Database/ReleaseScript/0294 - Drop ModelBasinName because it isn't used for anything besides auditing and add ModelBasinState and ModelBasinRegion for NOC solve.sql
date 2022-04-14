alter table dbo.ModelBasin
add ModelBasinState varchar(5) null

alter table dbo.ModelBasin
add ModelBasinRegion varchar(10) null
go

update dbo.ModelBasin
set ModelBasinState = 'ca',
ModelBasinRegion = 'soc'

alter table dbo.ModelBasin
alter column ModelBasinState varchar(5) not null

alter table dbo.ModelBasin
alter column ModelBasinRegion varchar(10) not null

alter table dbo.ModelBasin
drop column ModelBasinName

alter table dbo.ModelBasinStaging
add ModelBasinState varchar(5) not null

alter table dbo.ModelBasinStaging
add ModelBasinRegion varchar(10) not null

alter table dbo.ModelBasinStaging
drop column ModelBasinName