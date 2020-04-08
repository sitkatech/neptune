Drop view if exists dbo.vNereidTreatmentBMPRegionalSubbasin
go

Create view dbo.vNereidTreatmentBMPRegionalSubbasin
as
select
	Row_number() over(order by tbmp.TreatmentBMPID asc) as PrimaryKey, 
	tbmp.TreatmentBMPID,
	rsb.RegionalSubbasinID
from dbo.TreatmentBMP tbmp
	left join dbo.TreatmentBMPType tbmpt
		on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
	left join dbo.Delineation d
		on tbmp.TreatmentBMPID = d.TreatmentBMPID
	join  dbo.RegionalSubbasin rsb
		on tbmp.RegionalSubbasinID = rsb.RegionalSubbasinID
	left join dbo.vNereidBMPColocation bmpc
		on tbmp.TreatmentBMPID = bmpc.DownstreamBMPID
where
	tbmpt.TreatmentBMPModelingTypeID is not null
	and (d.DelineationTypeID is null or d.DelineationTypeID = 2)
	and IsInLSPCBasin = 1
	and DownstreamBMPID is null
go

select * from vNereidTreatmentBMPRegionalSubbasin