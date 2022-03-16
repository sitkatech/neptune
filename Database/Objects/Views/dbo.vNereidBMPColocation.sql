drop view if exists dbo.vNereidBMPColocation
GO

create view dbo.vNereidBMPColocation
as
select 
	bmp_down.TreatmentBMPID as PrimaryKey,
	bmp_down.TreatmentBMPID as DownstreamBMPID,
	--Include this for planned projects
	bmp_down.RegionalSubbasinID as DownstreamRSBID,
	bmp_up.TreatmentBMPID as UpstreamBMPID,
	--Include this for planned projects
	bmp_up.RegionalSubbasinID as UpstreamRSBID
from dbo.TreatmentBMP bmp_Down
	join dbo.TreatmentBMP bmp_up
	on bmp_down.UpstreamBMPID = bmp_up.TreatmentBMPID
	where bmp_Down.ProjectID is null and bmp_up.ProjectID is null
go
