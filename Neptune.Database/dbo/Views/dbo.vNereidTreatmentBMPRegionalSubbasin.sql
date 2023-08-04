Drop view if exists dbo.vNereidTreatmentBMPRegionalSubbasin
go

-- collects the Distributed Modeling BMPs together with the RSBs they flow to,
-- accounting for the upstream-BMP relationship
Create view dbo.vNereidTreatmentBMPRegionalSubbasin
as
select
	Row_number() over(order by tbmp.TreatmentBMPID asc) as PrimaryKey, 
	tbmp.TreatmentBMPID,
	rsb.RegionalSubbasinID,
	rsb.OCSurveyCatchmentID
from dbo.TreatmentBMP tbmp
	left join dbo.TreatmentBMPType tbmpt
		on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
	left join dbo.Delineation d
		on tbmp.TreatmentBMPID = d.TreatmentBMPID
	join  dbo.RegionalSubbasin rsb
		on tbmp.RegionalSubbasinID = rsb.RegionalSubbasinID
	left join dbo.vNereidBMPColocation bmpc
		-- I should not be included in the BMP->RSB flow if I am the upstream of another BMP
		on tbmp.TreatmentBMPID = bmpc.UpstreamBMPID
where
	tbmpt.TreatmentBMPModelingTypeID is not null
	and (d.DelineationTypeID is null or d.DelineationTypeID = 2)
	and IsInModelBasin = 1
	and DownstreamBMPID is null
	and tbmp.ProjectID is null
go
/*
select * from vNereidTreatmentBMPRegionalSubbasin
*/