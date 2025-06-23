Create view dbo.vTreatmentBMPModeledLandUseArea
as

with cteTreatmentBMPsWithHRUCharacteristics
as (
select vhru.TreatmentBMPID, Sum(Area) Area
from dbo.vHRUCharacteristic vhru
join dbo.Delineation d on vhru.TreatmentBMPID = d.TreatmentBMPID
where d.IsVerified = 1
group by vhru.TreatmentBMPID),

cteRegionalSubbasinsWithHRUCharacteristics
as (
select vrsu.PrimaryKey as RegionalSubbasinID, Sum(Area) Area
from  dbo.vRegionalSubbasinUpstream vrsu
left join (select RegionalSubbasinID, sum(Area) Area
	  from dbo.vHRUCharacteristic 
	  group by RegionalSubbasinID) RSBArea on vrsu.PrimaryKey = RSBArea.RegionalSubbasinID
group by PrimaryKey),

cteStandAloneTreatmentBMPsWithHRUCharacteristics as
(
	select TreatmentBMPID, Area
	from cteTreatmentBMPsWithHRUCharacteristics
	union
	select tbmp.TreatmentBMPID, Area
	from dbo.TreatmentBMP tbmp
	left join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
	left join dbo.Delineation d on tbmp.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.DelineationType dt on d.DelineationTypeID = dt.DelineationTypeID
	left join cteRegionalSubbasinsWithHRUCharacteristics cte on cte.RegionalSubbasinID = tbmp.RegionalSubbasinID
	where tbmp.RegionalSubbasinID is not null and 
		  tbmpt.IsAnalyzedInModelingModule = 1 and
		  dt.DelineationTypeName = 'Centralized'
		  and d.IsVerified = 1
)

select TreatmentBMPID, Area
from cteStandAloneTreatmentBMPsWithHRUCharacteristics
union 
select tbmp.TreatmentBMPID, Area
from dbo.TreatmentBMP tbmp
join dbo.vTreatmentBMPUpstream vtbmpu on tbmp.TreatmentBMPID = vtbmpu.TreatmentBMPID
join cteStandAloneTreatmentBMPsWithHRUCharacteristics cte on vtbmpu.UpstreamBMPID = cte.TreatmentBMPID

GO