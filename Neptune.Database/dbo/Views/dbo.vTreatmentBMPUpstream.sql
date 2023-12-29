Create view dbo.vTreatmentBMPUpstream
as

with cteBMPs as (
 select RegionalSubbasinID, TreatmentBMPID, TreatmentBMPName, TreatmentBMPTypeID, UpstreamBMPID, 1 as Depth
     from dbo.TreatmentBMP
 union all
 select r.RegionalSubbasinID, r.TreatmentBMPID, r.TreatmentBMPName, r.TreatmentBMPTypeID, c.UpstreamBMPID, c.Depth + 1
     from dbo.TreatmentBMP r
     join cteBMPs c on r.UpstreamBMPID = c.TreatmentBMPID
)
select TreatmentBMPID, UpstreamBMPID, Depth
from
(
    select cteBMPs.TreatmentBMPID, cteBMPs.UpstreamBMPID, cteBMPs.Depth, cteBMPs.RegionalSubbasinID, 
    rank() over (partition by cteBMPs.TreatmentBMPID order by cteBMPs.Depth desc) as Ranking
    from cteBMPs
    where UpstreamBMPID is not null
) a
where a.Ranking = 1
union 
select cteBMPs.TreatmentBMPID, cteBMPs.UpstreamBMPID, cteBMPs.Depth
from cteBMPs
where Depth = 1 and UpstreamBMPID is null

GO