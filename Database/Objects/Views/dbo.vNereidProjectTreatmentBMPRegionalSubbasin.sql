Drop view if exists dbo.vNereidProjectTreatmentBMPRegionalSubbasin
go

-- collects the Distributed Modeling BMPs together with the RSBs they flow to,
-- for any  projects
-- we don't allow upstream bmps in  projects so they should not be a concern here
Create view dbo.vNereidProjectTreatmentBMPRegionalSubbasin
as
select
	Row_number() over(order by tbmp.TreatmentBMPID asc) as PrimaryKey, 
	tbmp.TreatmentBMPID,
	tbmp.ProjectID,
	rsb.RegionalSubbasinID,
	rsb.OCSurveyCatchmentID
from dbo.TreatmentBMP tbmp
join dbo.RegionalSubbasin rsb
	on tbmp.RegionalSubbasinID = rsb.RegionalSubbasinID
left join dbo.TreatmentBMPType tbmpt
	on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
left join dbo.Delineation d
	on tbmp.TreatmentBMPID = d.TreatmentBMPID
where
	tbmpt.TreatmentBMPModelingTypeID is not null
	and (d.DelineationTypeID is null or d.DelineationTypeID = 2)
	and IsInModelBasin = 1
	and tbmp.ProjectID is not null
go

select * from vNereidProjectTreatmentBMPRegionalSubbasin