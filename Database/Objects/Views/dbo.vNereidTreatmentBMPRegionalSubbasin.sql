Drop view if exists dbo.vNereidTreatmentBMPRegionalSubbasin
go

Create view dbo.vNereidTreatmentBMPRegionalSubbasin
as
select
	Row_number() over(order by tbmp.TreatmentBMPID asc) as PrimaryKey, 
	tbmp.TreatmentBMPID,
	RegionalSubbasinID
from dbo.TreatmentBMP tbmp
	left join dbo.TreatmentBMPType tbmpt
		on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
	left join dbo.Delineation d
		on tbmp.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.RegionalSubbasin r
		on tbmp.LocationPoint.STWithin(r.CatchmentGeometry) = 1
where
	tbmpt.TreatmentBMPModelingTypeID is not null
	and d.DelineationTypeID != 1
